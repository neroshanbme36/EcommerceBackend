namespace Domain.Common
{
    public class BaseAddressDomainEposEntity : BaseEntity
    {
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