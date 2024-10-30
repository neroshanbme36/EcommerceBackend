using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CustomerAddressRepository : GenericRepository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly MainDbContext _dbContext;

        public CustomerAddressRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<CustomerAddress>> GetCustomerAddressesByEcommUserId(string ecommUserId)
        {
            return await _dbContext.CustomerAddresses.Where(c => c.EcommUserId == ecommUserId).ToListAsync();
        }

        public async Task<CustomerAddress?> GetDefaultCustomerAddress(string ecommUserId, string category)
        {
            return await _dbContext.CustomerAddresses.FirstOrDefaultAsync(c => c.EcommUserId == ecommUserId && c.Category == category && c.IsDefault);
        }
    }
}