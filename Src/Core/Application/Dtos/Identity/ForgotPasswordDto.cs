
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Identity
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}