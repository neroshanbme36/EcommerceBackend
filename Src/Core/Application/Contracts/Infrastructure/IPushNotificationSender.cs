using System.Threading.Tasks;
using Application.Models;

namespace Application.Contracts.Infrastructure
{
    public interface IPushNotificationSender
    {
        Task<Result> AddOrEditFcmUser(string userId, string fcmDeviceToken);
        Task<Result> DeleteFcmUser(string userId);
        Task<Result> SendMessage(string userId, string title, string body);
    }
}