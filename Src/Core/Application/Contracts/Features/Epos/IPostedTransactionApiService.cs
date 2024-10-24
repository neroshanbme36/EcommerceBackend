using System.Threading.Tasks;
using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IPostedTransactionApiService
    {
        Task<OrderDto?> GetRepairTransaction(string eposApiKey, string deviceId, string repairId);
    }
}