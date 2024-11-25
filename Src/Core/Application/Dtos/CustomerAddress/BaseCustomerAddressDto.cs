
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.CustomerAddress
{
    public class BaseCustomerAddressDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(50)]
        public string? AddressLine2 { get; set; }

        [StringLength(50)]
        public string? AddressLine3 { get; set; }

        [StringLength(50)]
        public string? AddressLine4 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PostCode { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string CountryId { get; set; } = string.Empty;
    }
}