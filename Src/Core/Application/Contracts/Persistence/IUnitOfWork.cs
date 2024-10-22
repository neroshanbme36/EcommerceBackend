using System;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
  public interface IUnitOfWork : IDisposable
  {
    Task<int> Save();
    bool HasChanges();
    //IStoreRepository StoreRepository { get; }
  }
}
