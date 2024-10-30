using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.WishlistProduct;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class WishlistProductsController : BaseApiController
    {
        private readonly IWishlistProductService _wishlistProductService;

        public WishlistProductsController(IWishlistProductService wishlistProductService)
        {
            _wishlistProductService = wishlistProductService;
        }

        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<WishlistProductDto>>> GetUserWishlistProducts()
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            return (await _wishlistProductService.GetWishlistProductsByUserId(userId)).ToList();
        }

        [HttpPost("add-wishlist-product")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddWishlistProduct(AddWistlistProductDto request)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            await _wishlistProductService.AddWishlistProduct(userId, request);
            return NoContent();
        }

        [HttpDelete("user/{itemNo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteWishlistProduct(string itemNo)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            await _wishlistProductService.DeleteWishlistProduct(userId, itemNo);
            return NoContent();
        }
    }
}