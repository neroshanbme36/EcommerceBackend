using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductVendorConfigurations : IEntityTypeConfiguration<ProductVendor>
    {
        public void Configure(EntityTypeBuilder<ProductVendor> builder)
        {
            builder.HasKey(iv => new { iv.ItemNo, iv.VendorId, iv.VendorItemNo });
            builder.Property(iv => iv.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(iv => iv.VendorId).HasMaxLength(20).IsRequired();
            builder.Property(iv => iv.VendorItemNo).HasMaxLength(20).IsRequired();
            builder.Property(iv => iv.Description).HasMaxLength(50).IsRequired();
            builder.Property(iv => iv.PackQuantity).HasColumnType("decimal(18,4)");
            builder.Property(iv => iv.PurchasePrice).HasColumnType("decimal(18,4)");
            builder.Property(iv => iv.Rrp).HasColumnType("decimal(18,4)");
            builder.Property(iv => iv.Profit).HasColumnType("decimal(18,4)");
            builder.Property(iv => iv.OBarcode).HasMaxLength(20);
            builder.Property(iv => iv.BlackListed).HasMaxLength(10);
            builder.Property(iv => iv.PriceType).HasMaxLength(2);
        }
    }
}