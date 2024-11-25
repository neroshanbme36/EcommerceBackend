using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IShippingZoneRepository  : IGenericRepository<ShippingZone>
    {
        Task<ShippingZone?> GetShippingZone(string countryId, string postcode);
    }
}