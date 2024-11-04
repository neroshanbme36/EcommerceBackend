using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class AppAccessTokenConfigurations : IEntityTypeConfiguration<AppAccessToken>
    {
        public void Configure(EntityTypeBuilder<AppAccessToken> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Token).HasMaxLength(1000).IsRequired();
        }
    }
}