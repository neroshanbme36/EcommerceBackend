
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Cart
{
    public class CartLineInputDto
    {
        [StringLength(450)]
        public string? CartId {get; set;}
        public int LineNo {get; set;}
        public bool LineStatus {get; set;}

        [Required]
        [StringLength(20)]
        public string? ItemNo { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [StringLength(50)]
        public string? FreeText {get; set;}
        public bool InfoCodeAuthorized {get; set;}
    }
}