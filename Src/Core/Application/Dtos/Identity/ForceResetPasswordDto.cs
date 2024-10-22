using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ForceResetPasswordDto
    {
        [Required]
        [MaxLength(450)]
        public string UserId {get; set;} = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string NewPassword {get; set;} = string.Empty;
    }
}