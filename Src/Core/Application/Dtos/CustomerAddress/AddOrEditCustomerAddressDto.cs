

using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.CustomerAddress
{
    public class AddOrEditCustomerAddressDto  : IValidatableObject
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
        [StringLength(20)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

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
        public string Postcode { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string CountryId { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            List<string> types = new List<string>{"Home", "Office"};
            bool isTypeExist = types.Any(c => Type.Contains(c));
            if (!isTypeExist)
            {
                ValidationResult validationResult = new ValidationResult(errorMessage: "Type is invalid", memberNames: new[] { "Type" });
                validationResults.Add(validationResult);
            }

            List<string> categories = new List<string>{"Billing","Delivery"};
            bool isCategoryExist = categories.Any(c => Category.Contains(c));
            if (!isCategoryExist)
            {
                ValidationResult validationResult = new ValidationResult(errorMessage: "Category is invalid", memberNames: new[] { "Category" });
                validationResults.Add(validationResult);
            }

            return validationResults;
        }
    }
}