using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure.Epos
{
    public interface IEposAccountApiService
    {
        Task<string> GetAccessToken(string eposApiKey, string deviceId);
    }
}