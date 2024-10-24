namespace Application.Dtos.Store
{
    public class CrmStoreDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AppCompanyId {get; set;} = string.Empty;
        public string? LogoImgUrl {get; set;}
        public string? VatRegistrationNo { get; set; }
        public string Type { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string? Phone { get; set; } 
        public string? Email { get; set; } 
        public bool IsActive { get; set; }
        public string CountryId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string Guid {get; set;} = string.Empty;
    }
}