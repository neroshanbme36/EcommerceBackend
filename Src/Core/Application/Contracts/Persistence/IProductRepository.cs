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
        Task<Pagination<Product>> GetProducts(ProductParams productParams);
    }
}