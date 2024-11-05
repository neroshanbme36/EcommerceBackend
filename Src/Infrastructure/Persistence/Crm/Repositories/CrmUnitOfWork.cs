using Application.Contracts.Persistence.Crm;
using Persistence.Extensions;
using System;
using System.Threading.Tasks;

namespace Persistence.Crm.Repositories
{
  public class CrmUnitOfWork : ICrmUnitOfWork
  {
    private readonly CrmDbContext _dbContext;
    private ICrmStoreRepository? _storeRepository;
    private IDeviceRepository? _deviceRepository;

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

    public ICrmStoreRepository StoreRepository =>
        _storeRepository ??= new CrmStoreRepository(_dbContext);

    public IDeviceRepository DeviceRepository =>
      _deviceRepository ??= new DeviceRepository(_dbContext);
  }
}
