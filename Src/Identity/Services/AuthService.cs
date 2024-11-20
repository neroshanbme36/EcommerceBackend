using Application.Constants;
using Application.Contracts.Identity;
using Application.Dtos.Identity;
using Application.Exceptions;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Infrastructure;
using Application.Models;
using Application.Dtos.Email;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Identity.Services
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IPushNotificationSender _pushNotificationSender;
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _mainUow;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IPushNotificationSender pushNotificationSender,
        IEmailSender emailSender,
        IUnitOfWork mainUow,
        IHttpContextAccessor httpContextAccessor)
    {
      _userManager = userManager;
      _jwtSettings = jwtSettings.Value;
      _signInManager = signInManager;
      _pushNotificationSender = pushNotificationSender;
      _emailSender = emailSender;
      _mainUow = mainUow;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);

      if (user == null)
        throw new NotFoundException("User doesn't exists", email);

      return await MapUserDto(user);
    }

    public async Task<UserDto> GetUserById(string id)
    {
      var user = await _userManager.FindByIdAsync(id);

      if (user == null)
        throw new NotFoundException("User doesn't exists", id);

      return await MapUserDto(user);
    }

    private async Task<UserDto> MapUserDto(ApplicationUser user)
    {
      string roleName = await GetRoleName(user);
      return MapUserDto(user, roleName);
    }

    private UserDto MapUserDto(ApplicationUser user, string roleName)
    {
      return new UserDto
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        UserName = user.UserName,
        PhoneNumber = user.PhoneNumber,
        RoleName = roleName,
      };
    }

    private async Task<string> GetRoleName(ApplicationUser user)
    {
      IList<string> roles = await _userManager.GetRolesAsync(user);
      return roles.Count > 0 ? roles[0] : string.Empty;
    }

    public async Task<IReadOnlyList<UserDto>> GetUsers(IReadOnlyList<string> ids)
    {
      var users = await _userManager.Users.Where(c => ids.Contains(c.Id)).ToListAsync();
      var userDtos = new List<UserDto>();
      foreach (var user in users)
      {
        userDtos.Add(await MapUserDto(user));
      }
      return userDtos;
    }

    private async Task<IReadOnlyList<UserDto>> GetUsersByRoleName(string roleName)
    {
      List<UserDto> userDtos = new List<UserDto>();
      IList<ApplicationUser> users = await _userManager.GetUsersInRoleAsync(roleName);
      foreach (var user in users)
      {
        userDtos.Add(MapUserDto(user, roleName));
      }
      return userDtos;
    }

    public async Task<IReadOnlyList<UserDto>> GetUsersByRoleNames(IReadOnlyList<string> roleNames)
    {
      List<UserDto> userDtos = new List<UserDto>();
      foreach (string roleName in roleNames)
      {
        userDtos.AddRange(await GetUsersByRoleName(roleName));
      }
      return userDtos;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);

      if (user == null)
        throw new UnauthorizedException("Invalid username or password");

      var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

      if (!result.Succeeded)
        throw new UnauthorizedException("Invalid username or password");

      if (!string.IsNullOrWhiteSpace(request.FcmDeviceToken))
      {
        Result pushNotificationResult = await _pushNotificationSender.AddOrEditFcmUser(user.Id, request.FcmDeviceToken);
        if (!pushNotificationResult.Success)
          throw new InternalServerErrorException("Registering push notification failed please try again");
      }

      AuthResponse response = new AuthResponse
      {
        Token = await GenerateToken(user)
      };

      return response;
    }

    public async Task Logout(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null) return;

      Result pushNotificationResult = await _pushNotificationSender.DeleteFcmUser(user.Id);
      if (!pushNotificationResult.Success)
        throw new InternalServerErrorException("Logout of push notification failed please try again");
    }

    public async Task<UserDto> Register(RegistrationDto request, string role)
    {
      var existingEmail = await _userManager.FindByEmailAsync(request.Email);

      if (existingEmail != null)
        throw new BadRequestException($"Email '{request.Email}' already exists.");

      var user = new ApplicationUser
      {
        Email = request.Email,
        FirstName = request.FirstName,
        LastName = request.LastName,
        UserName = request.Email,
        PhoneNumber = request.PhoneNumber,
        EmailConfirmed = true
      };

      var result = await _userManager.CreateAsync(user, request.Password);

      if (!result.Succeeded)
        throw new BadRequestException($"{GetIdentityErrorMessage(result.Errors)}");

      await _userManager.AddToRoleAsync(user, role);

      return await MapUserDto(user);
    }

    public async Task<UserDto> EditIdentityUser(EditIdentityUserDto request)
    {
      ApplicationUser userRepo = await _userManager.FindByIdAsync(request.Id);
      if (userRepo == null) throw new NotFoundException("User doesn't exists", request.Id);

      ApplicationUser existingEmail = await _userManager.FindByEmailAsync(userRepo.Email);
      if (existingEmail != null)
      {
        if (!string.Equals(existingEmail.Id, userRepo.Id)) throw new BadRequestException($"Email '{userRepo.Email}' already exists.");
      }

      //userRepo.Email = request.Email;
      userRepo.FirstName = request.FirstName;
      userRepo.LastName = request.LastName;
      //userRepo.UserName = request.Email;
      userRepo.PhoneNumber = request.PhoneNumber;

      IdentityResult result = await _userManager.UpdateAsync(userRepo);
      if (!result.Succeeded) throw new BadRequestException($"{GetIdentityErrorMessage(result.Errors)}");

      // IList<string> roles = await _userManager.GetRolesAsync(userRepo);
      // await _userManager.RemoveFromRolesAsync(userRepo, roles);
      // await _userManager.AddToRoleAsync(userRepo, request.Role);

      return await MapUserDto(userRepo);
    }

    public async Task ForceResetPassword(ForceResetPasswordDto request)
    {
      var user = await _userManager.FindByIdAsync(request.UserId);

      if (user == null)
        throw new BadRequestException("Force Reset Password failed");

      if (user.PasswordHash != null)
      {
        var removePasswordResult = await _userManager.RemovePasswordAsync(user);

        if (!removePasswordResult.Succeeded)
          throw new BadRequestException($"{GetIdentityErrorMessage(removePasswordResult.Errors)}");
      }

      var result = await _userManager.AddPasswordAsync(user, request.NewPassword);

      if (!result.Succeeded)
        throw new BadRequestException($"{GetIdentityErrorMessage(result.Errors)}");
    }

    public async Task ChangePassword(string email, ChangePasswordDto request)
    {
      var user = await _userManager.FindByEmailAsync(email);

      if (user == null)
        throw new BadRequestException("Change Password failed");

      var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

      if (!result.Succeeded)
        throw new BadRequestException($"{GetIdentityErrorMessage(result.Errors)}");
    }

    public async Task ForgotPassword(ForgotPasswordDto request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null) return;

      bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
      if (!isEmailConfirmed) return;

      string token = await _userManager.GeneratePasswordResetTokenAsync(user);

      Store store = await _mainUow.StoreRepository.GetTopStore();
      if (store == null) return;

      HttpRequest httpRequest = _httpContextAccessor.HttpContext.Request;
      string resetUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/reset-password?token={Uri.EscapeDataString(token)}&email={request.Email}";

      EmailDto emailDto = new EmailDto
      {
        SenderEmail = store.Email,
        ReceiverEmail = request.Email,
        Subject = "Password Reset",
        Body = $"Please reset your password by clicking <a href='{resetUrl}'>here</a>",
        IsBodyHtml = true
      };
      IReadOnlyList<EmailDto> emailDtos = new List<EmailDto> { emailDto };
      await _emailSender.SendEmail(emailDtos);
    }

    public async Task ResetPasswordEmail(ResetPasswordEmailDto request)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);
      if (user == null)  throw new BadRequestException("Reset Password failed");

      var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
      if (!result.Succeeded)
        throw new BadRequestException($"{GetIdentityErrorMessage(result.Errors)}");
    }

    private string GetIdentityErrorMessage(IEnumerable<IdentityError> errors)
    {
      return string.Join(",", errors.Select(c => c.Description));
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
      var userClaims = await _userManager.GetClaimsAsync(user);
      var roles = await _userManager.GetRolesAsync(user);

      var roleClaims = new List<Claim>();

      for (int i = 0; i < roles.Count; i++)
      {
        roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
      }

      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
      .Union(userClaims)
      .Union(roleClaims);

      var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

      var jwtSecurityToken = new JwtSecurityToken(
          issuer: _jwtSettings.Issuer,
          audience: _jwtSettings.Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
          signingCredentials: signingCredentials);

      string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
      return token;
    }
  }
}