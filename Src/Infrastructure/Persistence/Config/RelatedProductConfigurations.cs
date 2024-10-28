using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class RelatedProductConfigurations: IEntityTypeConfiguration<RelatedProduct>
    {
        public void Configure(EntityTypeBuilder<RelatedProduct> builder)
        {
            builder.Property(rp => rp.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(rp => rp.RelatedProductItemNo).HasMaxLength(20).IsRequired();

            builder.HasKey(rp => new { rp.ItemNo, rp.RelatedProductItemNo });
        }
    }
}