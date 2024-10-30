using Application.Dtos.CustomerAddress;

namespace Application.Contracts.Features
{
    public interface ICustomerAddressService
    {
        Task<IReadOnlyList<CustomerAddressDto>> GetCustomerAddressesByEcommUserId(string ecommUserId);
        Task<AddOrEditCustomerAddressDto> AddOrEditCustomerAddress(string ecommUserId, AddOrEditCustomerAddressDto request);
        Task DeleteCustomerAddress(string ecommUserId, long id);
    }
}