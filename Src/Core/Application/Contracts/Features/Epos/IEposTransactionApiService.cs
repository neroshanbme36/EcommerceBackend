using System.Threading.Tasks;
using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IEposTransactionApiService
    {
        Task<OrderDto?> GetRepairTransaction(string eposApiKey, string deviceId, string repairId);
        Task<OrderDto?> GetTransactionByGuid(string eposApiKey, string deviceId, string guid);
        Task<OrderDto?> InsertOrUpdateHeader(string eposApiKey, string deviceId, EposTransHeaderInputDto request);
        Task<ProductSearchResultDto?> InsertOrUpdateLine(string eposApiKey, string deviceId, EposTransLineInputDto request);
    }
}