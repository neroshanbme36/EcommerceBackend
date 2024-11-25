using System.ComponentModel.DataAnnotations;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.Cart
{
    public class BaseCartHeaderInputDto
    {
        [Required]
        [Range(1, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public OrderType OrderType { get; set; }

        public bool IsScheduledOrder { get; set; }
        public DateTime RequestedOn { get; set; }

        [StringLength(250)]
        public string? FreeText { get; set; }
    }
}