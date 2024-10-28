using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ItemNo);
            builder.Property(p => p.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Barcode).HasMaxLength(20);
            builder.Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Property(p => p.TakeawayPrice).HasColumnType("decimal(18,4)");
            builder.Property(p => p.DeliveryPrice).HasColumnType("decimal(18,4)");
            builder.Property(p => p.VendorItemNo).HasMaxLength(20);
            builder.Property(p => p.UnitSize).HasMaxLength(10);
            builder.Property(p => p.Rrp).HasColumnType("decimal(18,4)");
            builder.Property(p => p.UnitCost).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.Margin).HasColumnType("decimal(18,5)");
            builder.Property(p => p.OuterEan).HasMaxLength(20);
            builder.Property(p => p.Pum).HasMaxLength(20);
            builder.Property(p => p.PackCost).HasColumnType("decimal(18,4)");
            builder.Property(p => p.PackQuantity).HasColumnType("decimal(18,0)");
            builder.Property(p => p.PackDescription).HasMaxLength(50);
            builder.Property(p => p.StockQuantity).HasColumnType("decimal(18,0)");
            builder.Property(p => p.ReorderPoint).HasColumnType("decimal(18,0)");
            builder.Property(p => p.ReorderQuantity).HasColumnType("decimal(18,0)");
            builder.Property(P => P.MaxOrderQuantity).HasColumnType("decimal(18,0)");
            builder.Property(p => p.ParentItemNo).HasMaxLength(20);
            builder.Property(p => p.QuickProductId).HasMaxLength(20);
            builder.Property(p => p.Brand).HasMaxLength(50);
            builder.Property(p => p.ShelfNo).HasMaxLength(50);
            builder.Property(p => p.ShelfStaffId).HasMaxLength(50);
            builder.Property(p => p.ShelfRow).HasMaxLength(50);
            builder.Property(p => p.ShelfColumn).HasMaxLength(50);
            builder.Property(p => p.FullDescription).HasMaxLength(250);
            builder.Property(p => p.Type).HasMaxLength(20).IsRequired();
            builder.Property(p => p.OriginalPrice).HasColumnType("decimal(18,4)");
            builder.Property(p => p.DiscPercentage).HasColumnType("decimal(18,5)");

            builder.Property(p => p.DepartmentId).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Department).WithMany(d => d.Products).HasForeignKey(p => p.DepartmentId);

            builder.Property(p => p.VatId).HasMaxLength(10).IsRequired();
            builder.HasOne(p => p.Vat).WithMany(v => v.Products).HasForeignKey(p => p.VatId);

            builder.Property(p => p.ProductGroupId).HasMaxLength(50);
            builder.HasOne(p => p.ProductGroup).WithMany(pg => pg.Products).HasForeignKey(p => p.ProductGroupId);

            builder.Property(p => p.ProductSubGroupId).HasMaxLength(50);
            builder.HasOne(p => p.ProductSubGroup).WithMany(psg => psg.Products).HasForeignKey(p => p.ProductSubGroupId);

            builder.Property(p => p.VendorId).HasMaxLength(20);
            builder.HasOne(p => p.Vendor).WithMany(ve => ve.Products).HasForeignKey(p => p.VendorId);

            builder.HasMany(p => p.ProductBarcodes).WithOne(pb => pb.Product).OnDelete(DeleteBehavior.Restrict);
        }
    }
}