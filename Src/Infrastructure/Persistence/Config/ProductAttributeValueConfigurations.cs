using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductAttributeValueConfigurations : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.Property(pav => pav.ItemNo).HasMaxLength(20).IsRequired();

            builder.HasKey(pav => new {pav.ItemNo, pav.AttributeValueId});
            builder.HasOne(pav => pav.Product).WithMany(p => p.ProductAttributeValues).HasForeignKey(pav => pav.ItemNo);
            builder.HasOne(pav => pav.AttributeValue).WithMany(av => av.ProductAttributeValues).HasForeignKey(pav => pav.AttributeValueId);
        }
    }
}