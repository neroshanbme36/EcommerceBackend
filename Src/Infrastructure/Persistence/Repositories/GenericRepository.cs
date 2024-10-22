using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MainDbContext _dbContext;

        public GenericRepository(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> Get(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> Get(string id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public Task Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AttachRange(entities);
            foreach (T entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
