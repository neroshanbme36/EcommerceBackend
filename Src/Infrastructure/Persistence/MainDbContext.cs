using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  public class MainDbContext : AuditableDbContext
  {
    private string? connectionString;
    public string ConnectionString
    {
      set => connectionString = value;
      get => connectionString;
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // if (!optionsBuilder.IsConfigured)
      // {
      //     //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.    
      //     if (!string.IsNullOrWhiteSpace(ConnectionString)) optionsBuilder.UseSqlServer(ConnectionString);
      // }
      if (!string.IsNullOrWhiteSpace(connectionString)) optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
      optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // base.OnModelCreating(modelBuilder);
      // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      // modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackofficeDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
      //modelBuilder.ApplyConfiguration(new StoreConfigurations());
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Department> Departments {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<ProductDepartment> ProductDepartments {get; set;}
    public DbSet<Configuration> Configurations {get; set;}
    public DbSet<MediaFile> MediaFiles {get; set;}
  }
}
