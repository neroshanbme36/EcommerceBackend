using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PostedTransactionHeaderRepository : GenericRepository<PostedTransactionHeader>, IPostedTransactionHeaderRepository
    {
        private readonly MainDbContext _dbContext;

        public PostedTransactionHeaderRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostedTransactionHeader?> GetHeaderIncLines(long id)
        {
            return await _dbContext.PostedTransactionHeaders.AsNoTracking()
              .Include(c => c.PostedTransactionLines)
              .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AnyByEcommOrderId(long ecommOrderId)
        {
            return await _dbContext.PostedTransactionHeaders.AnyAsync(c => c.EcommOrderId == ecommOrderId);
        }
    }
}