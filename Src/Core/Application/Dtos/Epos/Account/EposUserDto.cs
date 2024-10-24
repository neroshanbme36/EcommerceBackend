using System;

namespace Application.Dtos.CloudStoreEpos.Epos.Account
{
    public class EposUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ModifiedOnGmt { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string CountryId { get; set; } //shape
    }
}