using System.ComponentModel.DataAnnotations;
using Application.Dtos.Cart;
using Application.Dtos.CustomerAddress;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.checkout
{
    public class CheckoutRequestDto : BaseCartHeaderInputDto, IValidatableObject
    {
        [Required]
        [StringLength(450)]
        public string CartId {get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        public BaseCustomerAddressDto? ShippingAddress {get; set;}
        public BaseCustomerAddressDto? BillingAddress {get; set;}

        [Required]
        [Range(1, 3, ErrorMessage = "Value for {0} must be between {1} and {2}.")]        
        public PaymentMethod PaymentMethod {get; set;} 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (IsScheduledOrder && RequestedOn < DateTime.Now)
            {
                ValidationResult validationResult = new ValidationResult(errorMessage: "Requested date time should be greater than current date time", memberNames: new[] { "RequestedOn" });
                validationResults.Add(validationResult);
            }
            
            if (OrderType == OrderType.Delivery && ShippingAddress == null)
            {
                ValidationResult validationResult = new ValidationResult(errorMessage: "Shipping address is required", memberNames: new[] { "ShippingAddress" });
                validationResults.Add(validationResult);
            }

            return validationResults;
        }
    }
}