using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class VendorConfigurations : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.Property(v => v.Id).HasMaxLength(20).IsRequired();
            builder.Property(v => v.Name).HasMaxLength(50).IsRequired();
            builder.Property(v => v.AddressLine1).HasMaxLength(50);
            builder.Property(v => v.AddressLine2).HasMaxLength(50);
            builder.Property(v => v.AddressLine3).HasMaxLength(50);
            builder.Property(v => v.AddressLine4).HasMaxLength(50);
            builder.Property(v => v.City).HasMaxLength(50);
            builder.Property(v => v.State).HasMaxLength(50);
            builder.Property(v => v.Postcode).HasMaxLength(50);
            builder.Property(v => v.Phone).HasMaxLength(20);
            builder.Property(v => v.Email).HasMaxLength(50);

            builder.Property(v => v.CountryId).HasMaxLength(10).IsRequired();
            builder.HasOne(u => u.Country).WithMany(v => v.Vendors).HasForeignKey(u => u.CountryId);

            builder.HasMany(v => v.Products).WithOne(p => p.Vendor).OnDelete(DeleteBehavior.Restrict);
        }
    }
}