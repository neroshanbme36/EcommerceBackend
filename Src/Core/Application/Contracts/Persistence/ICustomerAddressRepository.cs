using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ICustomerAddressRepository  : IGenericRepository<CustomerAddress>
    {
        Task<IReadOnlyList<CustomerAddress>> GetCustomerAddressesByEcommUserId(string ecommUserId);
        Task<CustomerAddress?> GetDefaultCustomerAddress(string ecommUserId, string category);
    }
}