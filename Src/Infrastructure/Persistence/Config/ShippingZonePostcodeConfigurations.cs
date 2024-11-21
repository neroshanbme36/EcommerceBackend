using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ShippingZonePostcodeConfigurations: IEntityTypeConfiguration<ShippingZonePostcode>
    {
        public void Configure(EntityTypeBuilder<ShippingZonePostcode> builder)
        {
            builder.Property(sp => sp.Postcode).HasMaxLength(20);
            builder.Property(sp => sp.PostcodePattern).HasMaxLength(20);
            builder.Property(sp => sp.PostcodeRangeStart).HasMaxLength(20);
            builder.Property(sp => sp.PostcodeRangeEnd).HasMaxLength(20);

            builder.HasOne(sp => sp.ShippingZone).WithMany(sz => sz.ShippingZonePostcodes).HasForeignKey(sp => sp.ShippingZoneId);
        }
    }
}