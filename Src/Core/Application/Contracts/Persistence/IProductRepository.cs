using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductHighlights();
        Task<IReadOnlyList<Product>> GetProductsByItemNos(IReadOnlyList<string> itemNos);
    }
}