using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Api.Middlewares.Builders;
using Application.Contracts.Features;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Models;
using Application.QueryParams;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class TransactionsController : BaseApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("posted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pagination<OrderDto>>> GetPostedTransactions([FromQuery] PostedTransHeaderParams headerParams)
        {
            string userId = HttpContext.User.RetrieveUserIdFromPrincipal();
            return await _transactionService.GetPostedTransactions(headerParams, userId);
        }
    }
}