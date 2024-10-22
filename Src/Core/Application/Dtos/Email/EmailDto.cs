namespace Application.Dtos.Email
{
    public class EmailDto
    {
        public string AppName {get; set;} = "Ecommerce";
        public string SenderEmail {get; set;} = string.Empty;
        public string ReceiverEmail {get; set;} = string.Empty;
        public string Subject {get; set;} = string.Empty;
        public string Body {get; set;} = string.Empty;
        public bool IsBodyHtml {get; set;}
    }
}