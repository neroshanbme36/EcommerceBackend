using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IAppAccessTokenRepository : IGenericRepository<AppAccessToken>
    {
        Task<AppAccessToken?> GetAccessTokenByName(string name);
    }
}