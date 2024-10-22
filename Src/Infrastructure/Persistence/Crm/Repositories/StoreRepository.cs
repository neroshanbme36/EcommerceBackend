using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Crm;
using Domain.Crm.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Crm.Repositories
{
  public class StoreRepository : CrmGenericRepository<Store>, IStoreRepository
  {
    private readonly CrmDbContext _dbContext;

    public StoreRepository(CrmDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Store>> GetStoresByCustomerId(string customerId)
    {
      return await _dbContext.Stores.Where(c => c.CustomerId == customerId).ToListAsync();
    }

    public async Task<IReadOnlyList<Store>> GetStoresByAppCompanyId(string appCompanyId)
    {
      return await _dbContext.Stores.Where(c => c.AppCompanyId == appCompanyId).ToListAsync();
    }

    public async Task<Store?> GetStoreByGuid(string guid)
    {
      return await _dbContext.Stores.FirstOrDefaultAsync(c => c.Guid == guid);
    }
  }
}