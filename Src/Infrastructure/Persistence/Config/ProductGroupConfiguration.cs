using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.Property(pg => pg.Id).HasMaxLength(50).IsRequired();
            builder.Property(pg => pg.Description).HasMaxLength(50);

            builder.HasMany(pg => pg.Products).WithOne(p => p.ProductGroup).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(pg => pg.ProductSubGroups).WithOne(pgs => pgs.ProductGroup).OnDelete(DeleteBehavior.Restrict);
        }
    }
}