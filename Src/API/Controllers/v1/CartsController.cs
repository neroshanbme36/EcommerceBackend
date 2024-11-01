using Api.Controllers.Common;
using Api.Errors;
using Api.Middlewares.Builders;
using Application.Constants;
using Application.Contracts.Features;
using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class CartsController : BaseApiController
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [AllowAnonymous]
        [HttpGet("cartId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDto>> GetCart(string cartId)
        {
            string? deviceId = HttpContext.Request.Headers[RequestHeaderCodes.DEVICE_ID];
            return await _cartService.GetCart(deviceId, cartId);
        }

        [AllowAnonymous]
        [HttpPost("add-or-edit-cart-header")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDto>> AddOrEditCartHeader(CartHeaderInputDto request)
        {
            string? deviceId = HttpContext.Request.Headers[RequestHeaderCodes.DEVICE_ID];
            return await _cartService.AddOrEditCartHeader(deviceId, request);
        }

        [AllowAnonymous]
        [HttpPost("add-or-edit-cart-line")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductSearchResultDto>> AddOrEditCartLine(CartLineInputDto request)
        {
            string? deviceId = HttpContext.Request.Headers[RequestHeaderCodes.DEVICE_ID];
            return await _cartService.AddOrEditCartLine(deviceId, request);
        }
    }
}