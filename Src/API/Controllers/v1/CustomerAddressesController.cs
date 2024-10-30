using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.CustomerAddress;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class CustomerAddressesController : BaseApiController
    {
        private readonly ICustomerAddressService _customerAddressService;

        public CustomerAddressesController(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<CustomerAddressDto>>> GetCustomerAddresses()
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            return (await _customerAddressService.GetCustomerAddressesByEcommUserId(userId)).ToList();
        }

        [HttpPost("add-or-edit-customer-address")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddOrEditCustomerAddressDto>> AddOrEditCustomerAddress(AddOrEditCustomerAddressDto request)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            await _customerAddressService.AddOrEditCustomerAddress(userId, request);
            return request;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCustomerAddress(long id)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            await _customerAddressService.DeleteCustomerAddress(userId, id);
            return NoContent();
        }
    }
}