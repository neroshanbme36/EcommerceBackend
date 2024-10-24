using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IPeripheralActionsApiService
    {
        Task PrintBookingReceipt(string eposApiKey, string deviceId, string repairId);
        Task PrintServiceReceipt(string eposApiKey, string deviceId, string repairId);
    }
}