namespace Application.Models
{
    public class MicroservicesBaseUrl
    {
        public string CurrentServerUrl {get; set;} = string.Empty;
        public string CurrentApiVersion {get; set;} = string.Empty;
        public string EposServerUrl {get; set;} = string.Empty;
        public string EposApiVersion {get; set;} = string.Empty;
    }
}