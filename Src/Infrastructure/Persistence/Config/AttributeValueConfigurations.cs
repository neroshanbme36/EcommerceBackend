using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class AttributeValueConfigurations : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.Property(av => av.Value).HasMaxLength(50).IsRequired();
            builder.Property(av => av.HexColor).HasMaxLength(20);

            builder.HasOne(av => av.Attribute).WithMany(a => a.AttributeValues).HasForeignKey(av => av.AttributeId);
        }
    }
}