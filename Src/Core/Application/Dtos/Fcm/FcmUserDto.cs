namespace Application.Dtos.Fcm
{
    public class FcmUserDto
    {
        public string AppAccessToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FcmDeviceToken { get; set; } = string.Empty;
    }
}