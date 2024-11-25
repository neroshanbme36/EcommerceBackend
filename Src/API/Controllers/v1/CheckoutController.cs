using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.checkout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class CheckoutController : BaseApiController
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckoutResponseDto>> ProcessCheckout(CheckoutRequestDto request)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            return await _checkoutService.ProcessCheckout(userId, request);
        }
    }
}