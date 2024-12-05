using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class EposTransactionHeaderRepository : GenericRepository<EposTransactionHeader>, IEposTransactionHeaderRepository
    {
        private readonly MainDbContext _dbContext;

        public EposTransactionHeaderRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EposTransactionHeader?> GetEposTransactionHeaderByGuid(string guid)
        {
            return await _dbContext.EposTransactionHeaders.FirstOrDefaultAsync(c => c.Guid == guid);
        }

        public async Task<EposTransactionHeader?> GetEposTransactionHeaderIncLinesByGuid(string guid)
        {
            return await _dbContext.EposTransactionHeaders
                .Include(c => c.EposTransactionLines)
                .FirstOrDefaultAsync(c => c.Guid == guid);
        }

        public async Task<bool> AnyByEcommOrderId(long ecommOrderId)
        {
            return await _dbContext.EposTransactionHeaders.AnyAsync(c => c.EcommOrderId == ecommOrderId);
        }
    }
}