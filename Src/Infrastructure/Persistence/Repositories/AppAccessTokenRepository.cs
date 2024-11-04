using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AppAccessTokenRepository : GenericRepository<AppAccessToken>, IAppAccessTokenRepository
    {
        private readonly MainDbContext _dbContext;

        public AppAccessTokenRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppAccessToken> GetAccessTokenByName(string name)
        {
            return await _dbContext.AppAccessTokens.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}