using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        Task<IReadOnlyList<Tag>> GetTags(IReadOnlyList<long> ids);
    }
}