
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class NumberSeriesConfiguration : IEntityTypeConfiguration<NumberSeries>
    {
        public void Configure(EntityTypeBuilder<NumberSeries> builder)
        {
            builder.Property(n => n.Id).ValueGeneratedNever();
            builder.Property(n => n.DeviceId).HasMaxLength(20).IsRequired();
            builder.Property(n => n.TableName).HasMaxLength(50).IsRequired();
            builder.Property(n => n.Prefix).HasMaxLength(2);
        }
    }
}