namespace Application.Models.Email
{
    public class EmailSettings
    {
        public string BaseAddress {get; set;} = string.Empty;
        public string AccessTokenEncryKey {get; set;} = string.Empty;
        public string AccessTokenClearTxt {get; set;} = string.Empty;
    }
}