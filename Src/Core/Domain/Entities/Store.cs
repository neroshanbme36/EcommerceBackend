using Domain.Common;

namespace Domain.Entities
{
    public class Store : BaseEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; } // company customer id
        public string Name { get; set; }
        public string VatRegistrationNo { get; set; }
        public string Type { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string? EcommerceUrl { get; set; }

        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}