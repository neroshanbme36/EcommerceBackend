using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class BarcodeMaskConfigurations : IEntityTypeConfiguration<BarcodeMask>
    {
        public void Configure(EntityTypeBuilder<BarcodeMask> builder)
        {
            builder.HasKey(bm => bm.Prefix);
            builder.Property(bm => bm.Prefix).HasMaxLength(15).IsRequired();
            builder.Property(bm => bm.Mask).HasMaxLength(22).IsRequired();

            builder.Property(bm => bm.ItemNo).HasMaxLength(20).IsRequired();
            builder.HasOne(bm => bm.Product).WithOne(p => p.BarcodeMask).HasForeignKey<BarcodeMask>(bm => bm.ItemNo);
        }
    }
}