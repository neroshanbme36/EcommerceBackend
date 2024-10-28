using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class MediaFileConfigurations : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.Property(src => src.Id).HasMaxLength(450);
            builder.Property(src => src.EntityType).HasMaxLength(50).IsRequired();
            builder.Property(src => src.EntityId).HasMaxLength(450);
            builder.Property(s => s.Type).HasMaxLength(50).IsRequired();
            builder.Property(src => src.Name).HasMaxLength(50).IsRequired();
            builder.Property(src => src.Extension).HasMaxLength(50).IsRequired();
            builder.Property(src => src.Path).HasMaxLength(1000).IsRequired();
            builder.Property(src => src.Url).HasMaxLength(1000).IsRequired();
        }
    }
}