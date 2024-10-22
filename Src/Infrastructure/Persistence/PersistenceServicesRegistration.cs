using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Crm.Repositories;
using Persistence.Repositories;

namespace Persistence
{
  public static class PersistenceServicesRegistration
  {
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
      string? crmConnectionString = configuration.GetConnectionString("CrmConnectionString");
      services.AddDbContext<CrmDbContext>(options =>
      {
        options.UseMySql(crmConnectionString, ServerVersion.AutoDetect(crmConnectionString));
      });

      string? mainConnectionString = configuration.GetConnectionString("ConnectionString");
      services.AddDbContext<MainDbContext>(options =>
      {
        options.UseMySql(mainConnectionString, ServerVersion.AutoDetect(mainConnectionString));
      });

      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddScoped(typeof(ICrmGenericRepository<>), typeof(CrmGenericRepository<>));
      services.AddScoped<ICrmUnitOfWork, CrmUnitOfWork>();

      return services;
    }
  }
}
