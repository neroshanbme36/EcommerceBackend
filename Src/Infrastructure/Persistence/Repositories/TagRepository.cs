using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TagRepository: GenericRepository<Tag>, ITagRepository
    {
        private readonly MainDbContext _dbContext;

        public TagRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Tag>> GetTags(IReadOnlyList<long> ids)
        {
            return await _dbContext.Tags.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}