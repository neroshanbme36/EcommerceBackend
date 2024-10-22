using Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class ApiVersioningServiceExtensions
    {
        // https://codewithmukesh.com/blog/api-versioning-in-aspnet-core-3-1/
        // https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/
        // https://www.telerik.com/blogs/your-guide-rest-api-versioning-aspnet-core
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            #region Api Versioning
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
                //Send customized error response when API version error.
                config.ErrorResponses = new ApiVersioningErrorResponseProvider();
            });
            #endregion
            return services;
        }
    }
}
