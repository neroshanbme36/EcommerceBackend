using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ShippingZoneConfigurations: IEntityTypeConfiguration<ShippingZone>
    {
        public void Configure(EntityTypeBuilder<ShippingZone> builder)
        {
            builder.Property(sp => sp.ZoneName).HasMaxLength(100).IsRequired();
            builder.Property(sp => sp.CountryId).HasMaxLength(10);
            builder.Property(sp => sp.ShippingCost).HasColumnType("decimal(18,4)");
            builder.Property(sp => sp.FreeShippingThreshold).HasColumnType("decimal(18,4)");

            builder.HasOne(sz => sz.country).WithMany(c => c.ShippingZones).HasForeignKey(sz => sz.CountryId);
            builder.HasMany(sz => sz.ShippingZonePostcodes).WithOne(sp => sp.ShippingZone).OnDelete(DeleteBehavior.Restrict);
        }
    }
}