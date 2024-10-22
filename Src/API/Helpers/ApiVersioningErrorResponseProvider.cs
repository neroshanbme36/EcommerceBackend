using Api.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Net;

namespace Api.Helpers
{
    // https://stackoverflow.com/questions/49124472/customized-error-responses-for-apiversioning-errors-in-webapi-dotnet-core
    public class ApiVersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        // note: in Web API the response type is HttpResponseMessage
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            var errorResponse = new ApiException(context.StatusCode, context.ErrorCode, context.Message);
            var response = new ObjectResult(errorResponse);
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return response;
        }
    }
}
