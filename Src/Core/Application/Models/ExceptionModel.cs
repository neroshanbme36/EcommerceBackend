namespace Application.Models
{
    public class ExceptionModel
    {
        public string? Source { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? TargetSite { get; set; }
        public string? StackTrace { get; set; }
    }
}