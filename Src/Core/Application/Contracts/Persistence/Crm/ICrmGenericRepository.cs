using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence.Crm
{
  public interface ICrmGenericRepository<T> where T : class
  {
    Task<T?> Get(long id);
    Task<T?> Get(string id);
    Task<IReadOnlyList<T>> GetAll();

    Task Add(T entity);
    Task AddRange(IEnumerable<T> entities);

    Task Update(T entity);
    Task UpdateRange(IEnumerable<T> entities);

    Task Delete(T entity);
    Task RemoveRange(IEnumerable<T> entities);
  }
}
