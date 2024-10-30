using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WishlistProductRepository : GenericRepository<WishlistProduct>, IWishlistProductRepository
    {
        private readonly MainDbContext _dbContext;

        public WishlistProductRepository(MainDbContext dbContext) : base(dbContext)
        {
             _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<WishlistProduct>> GetWishProductsByUserId(string userId)
        {
            return await _dbContext.WishlistProducts.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<WishlistProduct?> GetWishlistProductByUserIdAndItemNo(string userId, string itemNo)
        {
            return await _dbContext.WishlistProducts.FirstOrDefaultAsync(c => c.UserId == userId && c.ItemNo == itemNo);
        }

        public async Task<bool> IsAny(string userId, string itemNo)
        {
            return await _dbContext.WishlistProducts.AnyAsync(c => c.UserId == userId && c.ItemNo == itemNo);
        }
    }
}