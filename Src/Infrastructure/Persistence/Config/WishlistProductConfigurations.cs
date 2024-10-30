using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class WishlistProductConfigurations : IEntityTypeConfiguration<WishlistProduct>
    {
        public void Configure(EntityTypeBuilder<WishlistProduct> builder)
        {
            builder.HasKey(wp => new { wp.ItemNo, wp.UserId });
            builder.Property(wp => wp.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(wp => wp.UserId).HasMaxLength(450).IsRequired();

            builder.HasOne(wp => wp.Product).WithMany(p => p.WishlistProducts).HasForeignKey(wp => wp.ItemNo).OnDelete(DeleteBehavior.Cascade);
        }
    }
}