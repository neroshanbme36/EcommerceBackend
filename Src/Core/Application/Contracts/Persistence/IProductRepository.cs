using Application.Models;
using Application.QueryParams;
using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductHighlights();
        Task<IReadOnlyList<Product>> GetProductsByItemNos(IReadOnlyList<string> itemNos);
        Task<Product?> GetProductDetailByDescription(string description);
        Task<IReadOnlyList<Product>> GetRelatedProducts(string itemNo);
        Task<bool> IsAnyByItemNo(string itemNo);
        Task<Pagination<Product>> GetProducts(ProductParams productParams);
        Task<IReadOnlyList<Product>> GetWishlistProductsByUserId(string userId);
        Task<IReadOnlyList<Product>> GetProductsWithManageStockByItemNos(IReadOnlyList<string> itemNos);
    }
}