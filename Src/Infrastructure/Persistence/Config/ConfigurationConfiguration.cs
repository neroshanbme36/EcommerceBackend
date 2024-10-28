using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.DeviceId).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Type).HasMaxLength(20);
            builder.Property(c => c.Code).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Value).HasMaxLength(100);
            builder.Property(c => c.Detail).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Comments).HasMaxLength(100);
            builder.Property(c => c.ParentCode).HasMaxLength(100);
            builder.Property(c => c.CodeType).HasMaxLength(20).IsRequired();
        }
    }
}