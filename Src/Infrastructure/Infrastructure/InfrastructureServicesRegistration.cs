using Application.Contracts.Infrastructure;
using Application.Contracts.Infrastructure.Epos;
using Infrastructure.Helpers.RestClient;
using Infrastructure.Mail;
using Infrastructure.PushNotification;
using Infrastructure.Services.Epos;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
  public static class InfrastructureServicesRegistration
  {
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
      services.AddScoped<IRestClientV2, RestClientV2>();
      services.AddScoped<IEmailSender, EmailSender>();
      services.AddScoped<IPushNotificationSender, PushNotificationSender>();

      #region EPOS
      services.AddScoped<IEposTransactionApiService, EposTransactionApiService>();
      services.AddScoped<IEposPaymentApiService, EposPaymentApiService>();
      services.AddScoped<IPostedTransactionApiService, PostedTransactionApiService>();
      services.AddScoped<IEposAccountApiService, EposAccountApiService>();
      services.AddScoped<IPeripheralActionsApiService, PeripheralActionsApiService>();
      #endregion EPOS
      
      return services;
    }
  }
}
