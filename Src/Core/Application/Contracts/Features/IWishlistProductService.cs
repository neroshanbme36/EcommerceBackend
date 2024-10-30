using Application.Dtos.WishlistProduct;

namespace Application.Contracts.Features
{
    public interface IWishlistProductService
    {
        Task<IReadOnlyList<WishlistProductDto>> GetWishlistProductsByUserId(string userId);
        Task AddWishlistProduct(string userId, AddWistlistProductDto request);
        Task DeleteWishlistProduct(string userId, string itemNo);
    }
}