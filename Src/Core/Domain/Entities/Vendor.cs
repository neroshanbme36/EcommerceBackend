using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Vendor : BaseEntity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? AddressLine1 { get; set; } 
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string? City { get; set; } 
        public string? State { get; set; } 
        public string? Postcode { get; set; } 
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ModifiedOnGmt { get; set; }

        public string CountryId { get; set; } = string.Empty;
        public Country? Country { get; set; }

        public IReadOnlyList<Product> Products { get; set; } = new List<Product>();
    }
}