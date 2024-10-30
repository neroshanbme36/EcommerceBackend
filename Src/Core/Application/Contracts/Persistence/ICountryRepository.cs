using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<bool> IsAny(string id);
    }
}