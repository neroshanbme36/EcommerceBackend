using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductBarcodeConfiguration : IEntityTypeConfiguration<ProductBarcode>
    {
        public void Configure(EntityTypeBuilder<ProductBarcode> builder)
        {
            builder.HasKey(pb => pb.Barcode);
            builder.Property(pb => pb.Barcode).HasMaxLength(20).IsRequired();
            builder.Property(pb => pb.Description).HasMaxLength(50).IsRequired();
            builder.Property(pb => pb.Price).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.TakeawayPrice).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.DeliveryPrice).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.UnitSize).HasMaxLength(10);
            builder.Property(pb => pb.Colour).HasMaxLength(20);
            builder.Property(pb => pb.Rrp).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.UnitCost).HasColumnType("decimal(18,4)");
            builder.Property(pb => pb.Margin).HasColumnType("decimal(18,5)");
            builder.Property(pb => pb.PackCost).HasColumnType("decimal(18,4)");
            builder.Property(p => p.PackQuantity).HasColumnType("decimal(18,0)");

            builder.Property(pb => pb.ItemNo).HasMaxLength(20).IsRequired();
            builder.HasOne(pb => pb.Product).WithMany(p => p.ProductBarcodes).HasForeignKey(pb => pb.ItemNo);
        }
    }
}