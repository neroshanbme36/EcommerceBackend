using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.Bootstrap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class BootstrapController : BaseApiController
    {
        private readonly IBootstrapService _bootstrapService;

        public BootstrapController(IBootstrapService bootstrapService)
        {
            _bootstrapService = bootstrapService;
        }
        
        [AllowAnonymous]
        [HttpPost("prime-base")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PrimeBaseResponseDto>> GetPrimeBase(PrimeBaseRequestDto request)
        {
            string userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            return await _bootstrapService.GetPrimeBase(userEmail, request);
        }

        [AllowAnonymous]
        [HttpGet("homepage-resource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HomePageResourceDto>> GetHomePageResource()
        {   
            return await _bootstrapService.GetHomePageResource();
        }

        [AllowAnonymous]
        [HttpGet("browse-category-resource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrowseCategoryPageResourceDto>> GetBrowseCategoryPageResource()
        {   
            return await _bootstrapService.GetBrowseCategoryPageResource();
        }
    }
}