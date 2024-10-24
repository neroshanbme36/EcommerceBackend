using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
  public interface IStoreRepository  : IGenericRepository<Store>
  {
    Task<Store> GetTopStore();
  }
}
