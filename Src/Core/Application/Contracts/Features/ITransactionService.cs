using Application.Dtos.CloudStoreEpos.Epos;
using Application.Models;
using Application.QueryParams;

namespace Application.Contracts.Features
{
    public interface ITransactionService
    {
        Task<Pagination<OrderDto>> GetPostedTransactions(PostedTransHeaderParams headerParams, string userId);
    }
}