using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Identity;
using Application.Models.Identity;

namespace Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<UserDto> GetUserByEmail(string email);
        Task<AuthResponse> Login(AuthRequest request);
        Task<UserDto> Register(RegistrationDto request);
        Task ResetPassword(string email, ResetPasswordDto request);
        Task ForceResetPassword(ForceResetPasswordDto request);
        Task<IReadOnlyList<UserDto>> GetUsers(IReadOnlyList<string> ids);
        Task Logout(string email);
        Task<IReadOnlyList<UserDto>> GetUsersByRoleNames(IReadOnlyList<string> roleNames);
        Task<UserDto> GetUserById(string id);
        Task<UserDto> EditIdentityUser(EditIdentityUserDto request);
        Task ForgotPassword(string scheme, string host, ForgotPasswordDto request);
        Task ResetPasswordEmail(ResetPasswordEmailDto model);
    }
}