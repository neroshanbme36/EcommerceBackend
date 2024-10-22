using Domain.Crm.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Crm.Config;

namespace Persistence
{
  public class CrmDbContext : AuditableDbContext
  {
    public CrmDbContext(DbContextOptions<CrmDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // base.OnModelCreating(modelBuilder);
      // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      // modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackofficeDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new StoreConfigurations());
      modelBuilder.ApplyConfiguration(new DeviceConfigurations());
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Device> Devices {get; set;}
  }
}
