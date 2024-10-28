using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly MainDbContext _dbContext;

        public BrandRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Brand>> GetBrands()
        {
            return await _dbContext.Brands
                .Where(c => c.ShowInEcommerce)
                .ToListAsync();
        }
    }
}