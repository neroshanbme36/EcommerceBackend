using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ResetPasswordEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}