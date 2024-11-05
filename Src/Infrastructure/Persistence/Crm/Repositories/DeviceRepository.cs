using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence.Crm;
using Domain.Crm.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Crm.Repositories
{
    public class DeviceRepository : CrmGenericRepository<Device>, IDeviceRepository
    {
        private readonly CrmDbContext _dbContext;

        public DeviceRepository(CrmDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Device?> GetEcommerceDevice(string storeId)
        {
            return await _dbContext.Devices.FirstOrDefaultAsync(c => c.StoreId == storeId && c.Type == 2 && c.Description == "Ecommerce");
        }
    }
}