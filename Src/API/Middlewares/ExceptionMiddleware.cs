using Api.Errors;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
      
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // if there is no error pass to next middle ware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string json = string.Empty;

            switch (exception)
            {
                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    json = JsonSerializer.Serialize(new ApiResponse(context.Response.StatusCode, exception.Message), options);
                    break;
                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    json = JsonSerializer.Serialize(new ApiResponse(context.Response.StatusCode, exception.Message), options);
                    break;
                case BadRequestException badRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    json = JsonSerializer.Serialize(new ApiResponse(context.Response.StatusCode, exception.Message), options);
                    break;
                case InternalServerErrorException internalServerErrorException:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    json = JsonSerializer.Serialize(new ApiResponse(context.Response.StatusCode, exception.Message), options);
                    break;
                default:
                    _logger.LogError(exception, exception.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var apiException = new ApiException(context.Response.StatusCode, exception.Message, exception.StackTrace?.ToString());
                    json = JsonSerializer.Serialize(apiException, options);
                    break;
            }
             
            await context.Response.WriteAsync(json);
        }
    }
}