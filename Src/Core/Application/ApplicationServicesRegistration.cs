using Application.Contracts.Features;
using Application.Features;
using Application.Models;
using Application.Models.Email;
using Application.Models.PushNotification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<PushNotificationSettings>(configuration.GetSection("PushNotificationSettings"));
            services.Configure<MicroservicesBaseUrl>(configuration.GetSection("MicroservicesBaseUrl"));
            services.Configure<Content>(configuration.GetSection("Content"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IBootstrapService, BootstrapService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<ICustomerAddressService, CustomerAddressService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IWishlistProductService, WishlistProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAppAccessTokenService, AppAccessTokenService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IMediaFileService, MediaFileService>();
            return services;
        }
    }
}
