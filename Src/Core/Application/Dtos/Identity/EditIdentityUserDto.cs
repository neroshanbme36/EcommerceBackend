using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class EditIdentityUserDto
    {
        [Required]
        [StringLength(450)]
        public string Id {get; set;} = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string PhoneNumber {get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        // [Required]
        // [StringLength(20, MinimumLength = 8)]
        // public string Password {get; set;}

        [Required]
        [StringLength(20)]
        public string Role {get; set;} = string.Empty;
    }
}