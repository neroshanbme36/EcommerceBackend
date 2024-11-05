
namespace Application.Contracts.Features
{
    public interface IDeviceService
    {
        Task<string> GetEcommerceDeviceId();
    }
}