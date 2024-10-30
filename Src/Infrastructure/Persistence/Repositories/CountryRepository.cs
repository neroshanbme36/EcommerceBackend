
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly MainDbContext _dbContext;

        public CountryRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsAny(string id)
        {
            return await _dbContext.Countries.AnyAsync(c => c.Id == id);
        }
    }
}