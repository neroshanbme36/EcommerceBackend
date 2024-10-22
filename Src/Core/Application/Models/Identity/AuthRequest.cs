using System.ComponentModel.DataAnnotations;

namespace Application.Models.Identity
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        
        public string? FcmDeviceToken {get; set;}
    }
}