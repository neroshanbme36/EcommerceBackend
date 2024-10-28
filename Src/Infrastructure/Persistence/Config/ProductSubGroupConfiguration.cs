
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductSubGroupConfiguration : IEntityTypeConfiguration<ProductSubGroup>
    {
        public void Configure(EntityTypeBuilder<ProductSubGroup> builder)
        {
            builder.Property(psg => psg.Id).HasMaxLength(50).IsRequired();
            builder.Property(psg => psg.Description).HasMaxLength(50);

            builder.Property(psg => psg.ProductGroupId).HasMaxLength(50).IsRequired();
            builder.HasOne(psg => psg.ProductGroup).WithMany(ps => ps.ProductSubGroups).HasForeignKey(psg => psg.ProductGroupId);

            builder.HasMany(psg => psg.Products).WithOne(p => p.ProductSubGroup).OnDelete(DeleteBehavior.Restrict);
        }
    }
}