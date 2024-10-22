using Microsoft.AspNetCore.Builder;

namespace Api.Middlewares.Builders
{
    public class StoreEcommerceAuthMidBuilder
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<StoreEcommerceAuthMiddleware>();
        }
    }
}