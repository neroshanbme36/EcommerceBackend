namespace API.Middlewares.Builders
{
    public class StoreIdentityAuthMidBuilder
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<StoreIdentityAuthMiddleware>();
        }
    }
}