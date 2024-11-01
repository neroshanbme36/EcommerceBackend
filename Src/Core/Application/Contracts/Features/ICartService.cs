using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Features
{
    public interface ICartService
    {
        Task<OrderDto> GetCart(string deviceId, string cartId);
        Task<OrderDto> AddOrEditCartHeader(string deviceId, CartHeaderInputDto cartHeaderInputDto);
        Task<ProductSearchResultDto> AddOrEditCartLine(string deviceId, CartLineInputDto cartLineInputDto);
    }
}