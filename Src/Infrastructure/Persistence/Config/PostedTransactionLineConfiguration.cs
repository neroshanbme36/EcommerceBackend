using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
  public class PostedTransactionLineConfiguration : IEntityTypeConfiguration<PostedTransactionLine>
  {
    public void Configure(EntityTypeBuilder<PostedTransactionLine> builder)
    {
      builder.HasKey(l => new {l.HeaderId, l.LineNo});
      builder.Property(l => l.Barcode).HasMaxLength(20);
      builder.Property(l => l.KeyId).HasMaxLength(20).IsRequired();
      builder.Property(l => l.Description).HasMaxLength(50).IsRequired();
      builder.Property(l => l.Quantity).HasColumnType("decimal(18,0)");
      builder.Property(l => l.Price).HasColumnType("decimal(18,4)");
      builder.Property(l => l.Amount).HasColumnType("decimal(19,4)");
      builder.Property(l => l.DiscountPercentage).HasColumnType("decimal(18,5)");
      builder.Property(l => l.DiscountAmount).HasColumnType("decimal(19,4)");
      builder.Property(l => l.NetAmount).HasColumnType("decimal(19,4)");
      builder.Property(l => l.VatAmount).HasColumnType("decimal(19,4)");
      builder.Property(l => l.VatId).HasMaxLength(10);
      builder.Property(l => l.VatRate).HasColumnType("decimal(18,4)");
      builder.Property(l => l.DepartmentId).HasMaxLength(50);
      builder.Property(l => l.ProductGroupId).HasMaxLength(50);
      builder.Property(l => l.ProductSubGroupId).HasMaxLength(50);
      builder.Property(l => l.OfferId).HasMaxLength(20);
      builder.Property(l => l.OfferQuantity).HasColumnType("decimal(18,0)");
      builder.Property(l => l.MbId).HasMaxLength(20);
      builder.Property(l => l.UnitCost).HasColumnType("decimal(18,4)");
      builder.Property(l => l.TotalCost).HasColumnType("decimal(19,4)");
      builder.Property(l => l.ModifierTitle).HasMaxLength(100);
      builder.Property(l => l.ParentKeyId).HasMaxLength(20);
      builder.Property(l => l.FreeText).HasMaxLength(50);
      builder.Property(l => l.ZReportId).HasMaxLength(20);
      builder.Property(h => h.DeviceId).HasMaxLength(20).IsRequired();

      builder.HasOne(l => l.PostedTransactionHeader).WithMany(h => h.PostedTransactionLines).HasForeignKey(l => l.HeaderId);
    }
  }
}