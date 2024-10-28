using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<IReadOnlyList<Brand>> GetBrands();
    }
}