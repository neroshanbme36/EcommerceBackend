using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.WishlistProduct
{
    public class AddWistlistProductDto
    {
        [Required]
        [StringLength(20)]
        public string ItemNo {get; set;} = string.Empty;
    }
}