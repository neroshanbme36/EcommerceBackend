using System.Threading.Tasks;
using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using API.Middlewares.Builders;
using Application.Contracts.Identity;
using Application.Dtos.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreIdentityAuthMidBuilder))]
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Register(RegistrationDto request)
        {
            return await _authenticationService.Register(request);
        }

        // [HttpPost("reset-password")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult> ResetPassword(ResetPasswordDto request)
        // {
        //     string email = HttpContext.User.RetrieveEmailFromPrincipal();
        //     await _authenticationService.ResetPassword(email, request);
        //     return Ok();
        // }

        // [AllowAnonymous]
        // [HttpPost("force-reset-password")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        // public async Task<ActionResult> ForceResetPassword(ForceResetPasswordDto request)
        // {
        //     await _authenticationService.ForceResetPassword(request);
        //     return Ok();
        // }
        
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
            return await _authenticationService.GetUserByEmail(email);
        }
    }
}
