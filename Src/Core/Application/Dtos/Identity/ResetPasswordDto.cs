using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ResetPasswordDto
    {
        [Required]
        [StringLength(20)]
        public string OldPassword {get; set;} = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string NewPassword {get; set;} = string.Empty;
    }
}