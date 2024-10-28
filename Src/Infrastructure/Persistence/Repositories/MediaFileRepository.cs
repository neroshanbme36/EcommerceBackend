using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MediaFileRepository : GenericRepository<MediaFile>, IMediaFileRepository
    {
        private readonly MainDbContext _dbContext;

        public MediaFileRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<MediaFile>> GetMediaFilesByEntityTypeAndType(string entityType, string type)
        {
            return await _dbContext.MediaFiles
                .Where(c => c.EntityType == entityType && c.Type == type)
                .OrderBy(c => c.Priority)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MediaFile>> GetMediaFilesByEntityTypeEntityIdsType(string entityType, IReadOnlyList<string> types, IReadOnlyList<string> entityIds)
        {
            return await _dbContext.MediaFiles
                .Where(c => c.EntityType == entityType && types.Contains(c.Type) && entityIds.Contains(c.EntityId))
                .OrderBy(c => c.Priority)
                .ToListAsync();
        }
    }
}