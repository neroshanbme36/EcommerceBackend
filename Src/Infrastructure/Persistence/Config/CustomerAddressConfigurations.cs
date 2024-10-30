using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class CustomerAddressConfigurations : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(20);
            builder.Property(c => c.Category).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Type).HasMaxLength(20).IsRequired();
            builder.Property(c => c.EcommUserId).HasMaxLength(450);
            builder.Property(c => c.CustomerId).HasMaxLength(20);
            #region Address
            builder.Property(c => c.AddressLine1).HasMaxLength(50).IsRequired();
            builder.Property(c => c.AddressLine2).HasMaxLength(50);
            builder.Property(c => c.AddressLine3).HasMaxLength(50);
            builder.Property(c => c.AddressLine4).HasMaxLength(50);
            builder.Property(c => c.City).HasMaxLength(50).IsRequired();
            builder.Property(c => c.State).HasMaxLength(50).IsRequired();
            builder.Property(c => c.PostCode).HasMaxLength(50).IsRequired();
            #endregion Address

            builder.Property(c => c.CountryId).HasMaxLength(10).IsRequired();
            builder.HasOne(cu => cu.Country).WithMany(c => c.CustomerAddresses).HasForeignKey(cu => cu.CountryId);
        }
    }
}