using Api.Controllers.Common;
using Api.Errors;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.Product;
using Application.Models;
using Application.QueryParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pagination<ProductMinifyDto>>> GetProducts([FromQuery] ProductParams productParams)
        {
            return await _productService.GetProducts(productParams);
        }

        [AllowAnonymous]
        [HttpGet("search-by-slug/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDetailDto>> GetProductBySlug(string slug)
        {
            return await _productService.GetProductBySlug(slug);
        }
    }
}