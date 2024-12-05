using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class NumberSeriesRepository : GenericRepository<NumberSeries>, INumberSeriesRepository
    {
        private readonly MainDbContext _dbContext;

        public NumberSeriesRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NumberSeries?> GetNoSeriesByTbNameAndDevId(string tableName, string deviceId)
        {
            return await _dbContext.NumberSeries.FirstOrDefaultAsync(c => c.TableName == tableName && c.DeviceId == deviceId);
        }
    }
}