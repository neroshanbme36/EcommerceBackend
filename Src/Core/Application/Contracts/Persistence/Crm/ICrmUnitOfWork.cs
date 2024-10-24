using System;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence.Crm
{
  public interface ICrmUnitOfWork : IDisposable
  {
    Task<int> Save();
    bool HasChanges();
    ICrmStoreRepository StoreRepository { get; }
  }
}
