using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class CountryConfigurations : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Id).HasMaxLength(10).IsRequired();
            builder.Property(c => c.DisplayName).HasMaxLength(50).IsRequired();

            // builder.HasMany(c => c.Users).WithOne(u => u.Country).OnDelete(DeleteBehavior.Restrict);
            // builder.HasMany(c => c.Customers).WithOne(cu => cu.Country).OnDelete(DeleteBehavior.Restrict);
            // builder.HasMany(c => c.Stores).WithOne(s => s.Country).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Vendors).WithOne(v => v.Country).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.CustomerAddresses).WithOne(u => u.Country).OnDelete(DeleteBehavior.Restrict);
        }
    }
}