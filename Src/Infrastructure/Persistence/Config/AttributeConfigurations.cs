using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class AttributeConfigurations : IEntityTypeConfiguration<Domain.Entities.Attribute>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Attribute> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Style).HasMaxLength(20).IsRequired();

             builder.HasMany(a => a.AttributeValues).WithOne(av => av.Attribute).OnDelete(DeleteBehavior.Restrict);
        }
    }
}