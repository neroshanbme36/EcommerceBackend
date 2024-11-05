
using Domain.Crm.Entities;

namespace Application.Contracts.Features
{
    public interface IDeviceService
    {
        Task<string> GetEcommerceDeviceId();
        Task<Device?> GetEcommerceDevice();
    }
}