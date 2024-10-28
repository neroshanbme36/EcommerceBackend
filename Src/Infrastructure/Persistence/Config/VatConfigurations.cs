using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class VatConfigurations : IEntityTypeConfiguration<Vat>
    {
        public void Configure(EntityTypeBuilder<Vat> builder)
        {
            builder.Property(v => v.Id).HasMaxLength(10).IsRequired();
            builder.Property(v => v.Rate).HasColumnType("decimal(18,4)");

            builder.HasMany(v => v.Products).WithOne(p => p.Vat).OnDelete(DeleteBehavior.Restrict);
        }
    }
}