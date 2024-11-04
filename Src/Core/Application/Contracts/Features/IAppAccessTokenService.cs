using Domain.Entities;

namespace Application.Contracts.Features
{
    public interface IAppAccessTokenService
    {
        Task<AppAccessToken> GetAppAccessToken(string appName, Task<string> getAccessTokenFromServer);
        Task MakeTokenExpire(AppAccessToken appAccessToken);
    }
}