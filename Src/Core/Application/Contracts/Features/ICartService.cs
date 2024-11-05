using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Features
{
    public interface ICartService
    {
        Task<OrderDto> GetCart(string cartId);
        Task<OrderDto> AddOrEditCartHeader(CartHeaderInputDto cartHeaderInputDto);
        Task<ProductSearchResultDto> AddOrEditCartLine(CartLineInputDto cartLineInputDto);
    }
}