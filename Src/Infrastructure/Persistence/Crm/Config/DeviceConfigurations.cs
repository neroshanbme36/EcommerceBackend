using Domain.Crm.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Crm.Config
{
    public class DeviceConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(d => d.Id).HasMaxLength(20).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(50).IsRequired();
            builder.Property(d => d.IpAddress).HasMaxLength(50).IsRequired();
            builder.Property(d => d.Database).HasMaxLength(50).IsRequired();
            builder.Property(d => d.Username).HasMaxLength(50);
            builder.Property(d => d.Password).HasMaxLength(50).IsRequired();
            builder.Property(d => d.EposMiddlewareId).HasMaxLength(20);
            builder.Property(d => d.ProductKey).HasMaxLength(50);
            builder.Property(d => d.EndCode).HasMaxLength(200);
            builder.Property(d => d.DueCode).HasMaxLength(200);
            
            builder.Property(d => d.StoreId).IsRequired();
            builder.HasOne(d => d.Store).WithMany(s => s.Devices).HasForeignKey(d => d.StoreId);
        }
    }
}