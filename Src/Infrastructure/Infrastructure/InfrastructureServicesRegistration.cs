using Application.Contracts.Infrastructure;
using Infrastructure.Helpers.RestClient;
using Infrastructure.Mail;
using Infrastructure.PushNotification;
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
      return services;
    }
  }
}
