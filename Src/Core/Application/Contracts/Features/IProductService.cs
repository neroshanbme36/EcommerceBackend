
using Application.Dtos.Department;
using Application.Dtos.Product;

namespace Application.Contracts.Features
{
    public interface IProductService
    {
        Task<ProductHightlightsDto> GetProductHighlights();
        Task<IReadOnlyList<DepartmentProductMinifyDto>> GetHomepageDepartmentProducts();
    }
}