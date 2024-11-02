using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Specifications.Common;

namespace Application.Contracts.Persistence
{
  public interface IGenericRepository<T> where T : class
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
    Task<IReadOnlyList<T>> GetList(ISpecification<T> spec);
    Task<long> Count(ISpecification<T> spec);
  }
}
