using Application.Exceptions;
using Application.Helpers;
using Domain.Crm.Entities;
using Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Api.Middlewares
{
    public class StoreEcommerceAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public StoreEcommerceAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CrmDbContext _crmDbContext, MainDbContext _mainDbContext, IdentityDbContext _identityDbContext)
        {
            List<string> errorMessages = new List<string>();

            Store? store = await _crmDbContext.Stores.FirstOrDefaultAsync(c => c.EcommerceUrl == context.Request.Host.ToString());
            if (store == null) errorMessages.Add("Store doesnt exist");

            if (errorMessages.Count == 0)
            {
                var device = await _crmDbContext.Devices.FirstOrDefaultAsync(c => c.StoreId == store.Id && c.Type == 2 && c.Description == "Ecommerce");
                if (device == null)
                {
                    errorMessages.Add("Device doesnt exists");
                }
                else
                {
                    string database = "EcommAuth";
                    _identityDbContext.ConnectionString = SqlServerHelper.GetMySqlConnectionString(device.IpAddress, device.Port, database, device.Username, device.Password, false);
                    _mainDbContext.ConnectionString = SqlServerHelper.GetMySqlConnectionString(device.IpAddress, device.Port, device.Database, device.Username, device.Password, false);
                }
            }

            if (errorMessages.Count > 0)
                throw new BadRequestException(string.Join(", ", errorMessages));

            await _next(context); // if there is no error pass to next middle ware
        }
    }
}