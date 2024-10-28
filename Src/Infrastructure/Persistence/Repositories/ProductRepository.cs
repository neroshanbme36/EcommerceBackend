using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MainDbContext _dbContext;

        public ProductRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetProductHighlights()
        {
            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce && 
                (c.IsFeatured || c.IsTrending || c.IsTopSelling
                || c.IsTodayDeal || c.IsRecentlyAdded))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsByItemNos(IReadOnlyList<string> itemNos)
        {
            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce &&
                itemNos.Contains(c.ItemNo))
                .ToListAsync();
        }
    }
}