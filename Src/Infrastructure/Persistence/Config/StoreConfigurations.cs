using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class StoreConfigurations : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(s => s.Id).HasMaxLength(20).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
            builder.Property(s => s.VatRegistrationNo).HasMaxLength(50);
            builder.Property(s => s.Type).HasMaxLength(50).IsRequired();
            builder.Property(s => s.AddressLine1).HasMaxLength(50).IsRequired();
            builder.Property(s => s.AddressLine2).HasMaxLength(50);
            builder.Property(s => s.AddressLine3).HasMaxLength(50);
            builder.Property(s => s.AddressLine4).HasMaxLength(50);
            builder.Property(s => s.City).HasMaxLength(50).IsRequired();
            builder.Property(s => s.State).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Postcode).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Phone).HasMaxLength(20);
            builder.Property(s => s.Email).HasMaxLength(50);
            builder.Property(s => s.EcommerceUrl).HasMaxLength(250);

            builder.Property(s => s.CountryId).HasMaxLength(10).IsRequired();
            builder.HasOne(s => s.Country).WithMany(c => c.Stores).HasForeignKey(s => s.CountryId);
        }
    }
}