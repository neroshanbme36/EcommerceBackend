using System.Threading.Tasks;
using Application.Models;
using Application.Helpers;
using Application.Enums;
using Microsoft.Extensions.Options;
using Application.Models.PushNotification;
using Application.Dtos.Fcm;
using Application.Contracts.Infrastructure;

namespace Infrastructure.PushNotification
{
    public class PushNotificationSender : IPushNotificationSender
    {
        private readonly PushNotificationSettings _pushNotificationSettings;

        public PushNotificationSender(IOptions<PushNotificationSettings> optionPushNotificationSettings)
        {
            _pushNotificationSettings = optionPushNotificationSettings.Value;
        }

        public async Task<Result> AddOrEditFcmUser(string userId, string fcmDeviceToken)
        {
            FcmUserDto fcmUser = new FcmUserDto
            {
                AppAccessToken = _pushNotificationSettings.AccessToken,
                UserId = userId,
                FcmDeviceToken = fcmDeviceToken
            };

            using (var restClient = new RestClient(_pushNotificationSettings.BaseAddress))
            {
                RestClientResult rcResult = await restClient.Post("/api/v1/Fcm/add-or-edit-user", fcmUser);
                return new Result(rcResult.ApiStatus == ApiStatus.Success, rcResult.Data);
            }
        }

        public async Task<Result> DeleteFcmUser(string userId)
        {
            using (var restClient = new RestClient(_pushNotificationSettings.BaseAddress))
            {
                RestClientResult rcResult = await restClient.Delete($"/api/v1/Fcm/users/{_pushNotificationSettings.AccessToken}/{userId}");
                return new Result(rcResult.ApiStatus == ApiStatus.Success, rcResult.Data);
            }
        }

        public async Task<Result> SendMessage(string userId, string title, string body)
        {
            FcmMessageDto fcmMessage = new FcmMessageDto
            {
                AppAccessToken = _pushNotificationSettings.AccessToken,
                UserId = userId,
                Title = title,
                Body = body
            };

            using (var restClient = new RestClient(_pushNotificationSettings.BaseAddress))
            {
                RestClientResult rcResult = await restClient.Post("/api/v1/Fcm/send-message", fcmMessage);
                return new Result(rcResult.ApiStatus == ApiStatus.Success, rcResult.Data);
            }
        }
    }
}