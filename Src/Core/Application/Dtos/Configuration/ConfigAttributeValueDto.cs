namespace Application.Dtos.Configuration
{
    public class ConfigAttributeValueDto
    {
        public bool OnlineCardPayment {get; set;}
        public bool CashOnDelivery {get; set;}
        public string? PlayStore {get; set;} 
        public string? AppStore {get; set;}
        public string? FaceBook {get; set;}
        public string? Instagram {get; set;}
        public string? Twitter {get; set;}
        public string? PrimaryColor {get; set;}
        public string? HoverColor {get; set;}
        public string? FooterColor {get; set;}
        public string? SideMenuColor {get; set;}
    }
}