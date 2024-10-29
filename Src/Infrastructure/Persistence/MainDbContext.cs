using Domain.Entities;
using Domain.Entities.CloudStore;
using Microsoft.EntityFrameworkCore;
using Persistence.Config;

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
      modelBuilder.ApplyConfiguration(new AttributeConfigurations());
      modelBuilder.ApplyConfiguration(new AttributeValueConfigurations());
      modelBuilder.ApplyConfiguration(new BarcodeMaskConfigurations());
      modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
      modelBuilder.ApplyConfiguration(new CountryConfigurations());
      modelBuilder.ApplyConfiguration(new CrossSellProductConfigurations());
      modelBuilder.ApplyConfiguration(new DepartmentConfigurations());
      modelBuilder.ApplyConfiguration(new MediaFileConfigurations());
      modelBuilder.ApplyConfiguration(new ProductAttributeValueConfigurations());
      modelBuilder.ApplyConfiguration(new ProductBarcodeConfiguration());
      modelBuilder.ApplyConfiguration(new ProductConfiguration());
      modelBuilder.ApplyConfiguration(new ProductDepartmentConfigurations());
      modelBuilder.ApplyConfiguration(new ProductGroupConfiguration());
      modelBuilder.ApplyConfiguration(new ProductSubGroupConfiguration());
      modelBuilder.ApplyConfiguration(new ProductTagConfigurations());
      modelBuilder.ApplyConfiguration(new ProductVendorConfigurations());
      modelBuilder.ApplyConfiguration(new RelatedProductConfigurations());
      modelBuilder.ApplyConfiguration(new StoreConfigurations());
      modelBuilder.ApplyConfiguration(new TagConfigurations());
      modelBuilder.ApplyConfiguration(new VatConfigurations());
      modelBuilder.ApplyConfiguration(new VendorConfigurations());
      modelBuilder.ApplyConfiguration(new BrandConfigurations());
    }

    public DbSet<Domain.Entities.Attribute> Attributes { get; set; }
    public DbSet<AttributeValue> AttributeValues { get; set; }
    public DbSet<BarcodeMask> BarcodeMasks { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CrossSellProduct> CrossSellProducts { get; set; }
    public DbSet<Department> Departments { get; set; }
    // public DbSet<EposTransactionHeader> EposTransactionHeaders { get; set; }
    // public DbSet<EposTransactionLine> EposTransactionLines { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    // public DbSet<PostedTransactionHeader> PostedTransactionHeaders { get; set; }
    // public DbSet<PostedTransactionLine> PostedTransactionLines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
    public DbSet<ProductBarcode> ProductBarcodes { get; set; }
    public DbSet<ProductDepartment> ProductDepartments { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductSubGroup> ProductSubGroups { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<ProductVendor> ProductVendors { get; set; }
    public DbSet<RelatedProduct> RelatedProducts { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Vat> Vats { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
  }
}
