using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).HasMaxLength(50).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(50);
            builder.Property(d => d.ParentId).HasMaxLength(50);
            builder.Property(d => d.LongDescription).HasMaxLength(250);

            builder.HasMany(d => d.Products).WithOne(p => p.Department).OnDelete(DeleteBehavior.Restrict);
        }
    }
}