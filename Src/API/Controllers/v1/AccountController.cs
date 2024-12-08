using System.Threading.Tasks;
using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Constants;
using Application.Contracts.Features;
using Application.Contracts.Identity;
using Application.Dtos.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authenticationService;
        private readonly IWishlistProductService _wishlistProductService;

        public AccountController(IAuthService authenticationService, IWishlistProductService wishlistProductService)
        {
            _authenticationService = authenticationService;
            _wishlistProductService = wishlistProductService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Register(RegistrationDto request)
        {
            return await _authenticationService.Register(request, RoleNames.EcommerceCustomer);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return await _authenticationService.Login(request);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Logout()
        {
            string email = HttpContext.User.RetrieveEmailFromPrincipal();
            await _authenticationService.Logout(email);
            return Ok();
        }

        [HttpGet("getCurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            string email = HttpContext.User.RetrieveEmailFromPrincipal();
            UserDto user = await _authenticationService.GetUserByEmail(email);
            user.WishlistProducts = await _wishlistProductService.GetWishlistProductsByUserId(user.Id);
            return user;
        }
        
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto request)
        {
            await _authenticationService.ForgotPassword(request);
            return NoContent();
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ResetPasswordEmail(ResetPasswordEmailDto request)
        {
            await _authenticationService.ResetPasswordEmail(request);
            return NoContent();
        }

        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto request)
        {
            string email = HttpContext.User.RetrieveEmailFromPrincipal();
            await _authenticationService.ChangePassword(email, request);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPut("edit-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> EditIdentityUser(EditIdentityUserDto request)
        {
            UserDto user = await _authenticationService.EditIdentityUser(request);
            user.WishlistProducts = await _wishlistProductService.GetWishlistProductsByUserId(user.Id);
            return user;
        }
    }
}
