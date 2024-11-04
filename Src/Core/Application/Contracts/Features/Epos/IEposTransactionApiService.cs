using System.Threading.Tasks;
using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IEposTransactionApiService
    {
        Task<OrderDto?> GetTransactionByGuid(string token, string eposApiKey, string deviceId, string guid);
        Task<OrderDto?> InsertOrUpdateHeader(string token, string eposApiKey, string deviceId, EposTransHeaderInputDto request);
        Task<ProductSearchResultDto?> InsertOrUpdateLine(string token, string eposApiKey, string deviceId, EposTransLineInputDto request);
    }
}