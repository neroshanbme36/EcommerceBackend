using Application.Contracts.Persistence.Crm;
using Persistence.Extensions;
using System;
using System.Threading.Tasks;

namespace Persistence.Crm.Repositories
{
  public class CrmUnitOfWork : ICrmUnitOfWork
  {
    private readonly CrmDbContext _dbContext;
    private IStoreRepository? _storeRepository;

    public CrmUnitOfWork(CrmDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool HasChanges()
    {
      return _dbContext.HasChanges();
    }

    public async Task<int> Save()
    {
      return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
      _dbContext.Dispose();
      GC.SuppressFinalize(this);
    }

    public IStoreRepository StoreRepository =>
        _storeRepository ??= new StoreRepository(_dbContext);
  }
}
