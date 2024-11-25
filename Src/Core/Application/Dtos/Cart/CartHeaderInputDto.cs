
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Cart
{
    public class CartHeaderInputDto : BaseCartHeaderInputDto
    {
        [StringLength(450)]
        public string? CartId {get; set;}

        [StringLength(20)]
        public string? LocationId {get; set;}
        public int TableId {get; set;}
        public int Seats {get; set;}

        [StringLength(100)]
        public string? LoyaltyCardNo {get; set;} 
    }
}