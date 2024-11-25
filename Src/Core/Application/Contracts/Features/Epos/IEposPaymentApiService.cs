using System.Threading.Tasks;
using Application.Dtos.CloudStoreEpos.Epos.Payment;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IEposPaymentApiService
    {
         Task<PaymentResultDto?> PostPayment(string token, string eposApiKey, string deviceId, PaymentDto request);
    }
}