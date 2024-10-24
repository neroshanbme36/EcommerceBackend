using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private readonly MainDbContext _dbContext;

        public StoreRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Store> GetTopStore()
        {
            return await _dbContext.Stores.FirstOrDefaultAsync();
        }
    }
}