namespace Application.Dtos.Fcm
{
    public class FcmMessageDto
    {
        public string AppAccessToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}