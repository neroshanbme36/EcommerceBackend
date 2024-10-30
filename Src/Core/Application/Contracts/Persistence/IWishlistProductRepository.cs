using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IWishlistProductRepository : IGenericRepository<WishlistProduct>
    {
        Task<IReadOnlyList<WishlistProduct>> GetWishProductsByUserId(string userId);
        Task<WishlistProduct?> GetWishlistProductByUserIdAndItemNo(string userId, string itemNo);
        Task<bool> IsAny(string userId, string itemNo);
    }
}