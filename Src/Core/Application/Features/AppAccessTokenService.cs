using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Domain.Entities;

namespace Application.Features
{
    public class AppAccessTokenService : IAppAccessTokenService
    {
        private readonly IUnitOfWork _storeUow;

        public AppAccessTokenService(IUnitOfWork storeUow)
        {
            _storeUow = storeUow;
        }

        public async Task<AppAccessToken> GetAppAccessToken(string appName, Task<string> getAccessTokenFromServer)
        {
            AppAccessToken? appAccessToken = await _storeUow.AppAccessTokenRepository.GetAccessTokenByName(appName);
            if (appAccessToken != null)
            {
                if (appAccessToken.ExpiresOn > DateTime.Now) return appAccessToken;
            }
            else
            {
                appAccessToken = new AppAccessToken
                {
                    Name = appName
                };
            }
            appAccessToken.Token = await getAccessTokenFromServer;
            appAccessToken.ExpiresOn = DateTime.Now.AddHours(23);
            await AddOrEditAccessTokenWithoutSave(appAccessToken);
            return appAccessToken;
        }

        public async Task MakeTokenExpire(AppAccessToken appAccessToken)
        {
            appAccessToken.ExpiresOn = DateTime.Now.AddSeconds(-1);
            await AddOrEditAccessTokenWithoutSave(appAccessToken);
        }

        private async Task AddOrEditAccessTokenWithoutSave(AppAccessToken appAccessToken)
        {
            if (appAccessToken.Id == 0)
            {
                appAccessToken.CreatedOn = DateTime.Now;
                await _storeUow.AppAccessTokenRepository.Add(appAccessToken);
            }
            else
            {
                appAccessToken.UpdatedOn = DateTime.Now;
                await _storeUow.AppAccessTokenRepository.Update(appAccessToken);
            }
        }
    }
}