using Application.Contracts.Persistence;
using Persistence.Extensions;
using System;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly MainDbContext _dbContext;
    private IStoreRepository? _storeRepository;
    private IDepartmentRepository? _departmentRepository;
    private IConfigurationRepository? _configurationRepository;

    public UnitOfWork(MainDbContext dbContext)
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

    public IDepartmentRepository DepartmentRepository =>
      _departmentRepository ??= new DepartmentRepository(_dbContext);

    public IConfigurationRepository ConfigurationRepository =>
      _configurationRepository ??= new ConfigurationRepository(_dbContext);
  }
}
