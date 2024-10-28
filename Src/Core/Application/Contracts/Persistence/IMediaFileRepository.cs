using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IMediaFileRepository : IGenericRepository<MediaFile>
    {
        Task<IReadOnlyList<MediaFile>> GetMediaFilesByEntityTypeAndType(string entityType, string type);
        Task<IReadOnlyList<MediaFile>> GetMediaFilesByEntityTypeEntityIdsType(string entityType, IReadOnlyList<string> types, IReadOnlyList<string> entityIds);
    }
}