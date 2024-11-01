
using System.ComponentModel.DataAnnotations;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.Cart
{
    public class CartHeaderInputDto
    {
        [StringLength(450)]
        public string? CartId {get; set;}

        [Required]
        [Range(1, 2, ErrorMessage = "OrderType must be between 1 to 2")]
        public OrderType OrderType {get; set;}
        
        public bool IsScheduledOrder {get; set;}
        public DateTime RequestedOn {get; set;}

        [StringLength(20)]
        public string? LocationId {get; set;}
        public int TableId {get; set;}
        public int Seats {get; set;}

        [StringLength(250)]
        public string? FreeText {get; set;} 

         [StringLength(100)]
        public string? LoyaltyCardNo {get; set;} 
    }
}