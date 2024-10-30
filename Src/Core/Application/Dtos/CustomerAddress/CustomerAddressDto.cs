
namespace Application.Dtos.CustomerAddress
{
    public class CustomerAddressDto 
    {
        public long Id {get; set;}
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone {get; set;}
        public string Category {get; set;} = string.Empty;
        public string Type {get; set;}  = string.Empty;
        public bool IsDefault {get; set;}
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string CountryId { get; set; } = string.Empty;
    }
}