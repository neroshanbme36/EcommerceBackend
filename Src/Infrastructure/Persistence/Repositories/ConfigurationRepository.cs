using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        private readonly MainDbContext _dbContext;

        public ConfigurationRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Configuration>> GetConfigurationsByDeviceIdAndCommon(string deviceId)
        {
            return await _dbContext.Configurations.Where(c => c.DeviceId == "common" || c.DeviceId == deviceId).ToListAsync();
        }
    }
}