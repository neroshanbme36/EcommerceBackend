using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class CrossSellProductConfigurations : IEntityTypeConfiguration<CrossSellProduct>
    {
        public void Configure(EntityTypeBuilder<CrossSellProduct> builder)
        {
            builder.Property(csp => csp.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(csp => csp.CrossSellItemNo).HasMaxLength(20).IsRequired();

            builder.HasKey(csp => new { csp.ItemNo, csp.CrossSellItemNo });
        }
    }
}