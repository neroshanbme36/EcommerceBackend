using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public abstract class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedOn = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedOn = DateTime.UtcNow;
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
