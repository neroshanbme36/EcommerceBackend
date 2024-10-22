using System.Security.Claims;
using Application.Constants;

namespace Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        }

        public static string RetrieveUserIdFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(CustomClaimTypes.Uid)  ?? string.Empty;
        }
    }
}