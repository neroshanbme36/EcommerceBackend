using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
  public class PostedTransactionHeaderConfiguration : IEntityTypeConfiguration<PostedTransactionHeader>
  {
    public void Configure(EntityTypeBuilder<PostedTransactionHeader> builder)
    {
      builder.Property(h => h.Id).ValueGeneratedNever();
      builder.Property(h => h.StoreId).HasMaxLength(20).IsRequired();
      builder.Property(h => h.DeviceId).HasMaxLength(20).IsRequired();
      builder.Property(h => h.UserId).HasMaxLength(20).IsRequired();
      builder.Property(h => h.LocationId).HasMaxLength(20);
      builder.Property(h => h.LocationDesc).HasMaxLength(50);
      builder.Property(h => h.FreeText).HasMaxLength(250);
      builder.Property(h => h.CustomerId).HasMaxLength(20);
      builder.Property(h => h.LoyaltyCardNo).HasMaxLength(100);
      builder.Property(h => h.Provider).HasMaxLength(100);
      builder.Property(h => h.MerchantReceiptNo).HasMaxLength(100);
      builder.Property(h => h.CustomerRecepitNo).HasMaxLength(100);
      builder.Property(h => h.ZReportId).HasMaxLength(20);
      builder.Property(h => h.ShiftReportId).HasMaxLength(20);
      builder.Property(h => h.RepairId).HasMaxLength(20);
      builder.Property(h => h.StaffName).HasMaxLength(100).IsRequired();
      builder.Property(h => h.SalesAmount).HasColumnType("decimal(19,4)");
      builder.Property(c => c.EcommCustomerId).HasMaxLength(450);
      builder.Property(c => c.CustFirstName).HasMaxLength(50);
      builder.Property(c => c.CustLastName).HasMaxLength(50);
      builder.Property(c => c.CustPhone).HasMaxLength(20);
      builder.Property(c => c.CustEmail).HasMaxLength(50);
      builder.Property(c => c.BillAddressLine1).HasMaxLength(50);
      builder.Property(c => c.BillAddressLine2).HasMaxLength(50);
      builder.Property(c => c.BillAddressLine3).HasMaxLength(50);
      builder.Property(c => c.BillAddressLine4).HasMaxLength(50);
      builder.Property(c => c.BillCity).HasMaxLength(50);
      builder.Property(c => c.BillState).HasMaxLength(50);
      builder.Property(c => c.BillPostcode).HasMaxLength(50);
      builder.Property(c => c.BillCountryId).HasMaxLength(10);
      builder.Property(c => c.DeliAddressLine1).HasMaxLength(50);
      builder.Property(c => c.DeliAddressLine2).HasMaxLength(50);
      builder.Property(c => c.DeliAddressLine3).HasMaxLength(50);
      builder.Property(c => c.DeliAddressLine4).HasMaxLength(50);
      builder.Property(c => c.DeliCity).HasMaxLength(50);
      builder.Property(c => c.DeliState).HasMaxLength(50);
      builder.Property(c => c.DeliPostcode).HasMaxLength(50);
      builder.Property(c => c.DeliCountryId).HasMaxLength(10);
      builder.Property(c=> c.Guid).HasMaxLength(450);

      builder.HasMany(h => h.PostedTransactionLines).WithOne(l => l.PostedTransactionHeader).OnDelete(DeleteBehavior.Restrict);
    }
  }
}