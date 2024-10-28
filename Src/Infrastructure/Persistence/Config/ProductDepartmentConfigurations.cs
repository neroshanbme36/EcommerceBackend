using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class ProductDepartmentConfigurations : IEntityTypeConfiguration<ProductDepartment>
    {
        public void Configure(EntityTypeBuilder<ProductDepartment> builder)
        {
            builder.Property(pd => pd.ItemNo).HasMaxLength(20).IsRequired();
            builder.Property(pd => pd.DepartmentId).HasMaxLength(50).IsRequired();

            builder.HasKey(pd => new { pd.ItemNo, pd.DepartmentId });
            builder.HasOne(pd => pd.Product).WithMany(p => p.ProductDepartments).HasForeignKey(pd => pd.ItemNo);
            builder.HasOne(pd => pd.Department).WithMany(d => d.ProductDepartments).HasForeignKey(pd => pd.DepartmentId);
        }
    }
}