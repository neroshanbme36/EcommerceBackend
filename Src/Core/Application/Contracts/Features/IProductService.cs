
using Application.Dtos.Department;
using Application.Dtos.Product;
using Application.Models;
using Application.QueryParams;

namespace Application.Contracts.Features
{
    public interface IProductService
    {
        Task<ProductHightlightsDto> GetProductHighlights();
        Task<IReadOnlyList<DepartmentProductMinifyDto>> GetHomepageDepartmentProducts();
        Task<Pagination<ProductMinifyDto>> GetProducts(ProductParams productParams);
        Task<ProductDetailDto> GetProductBySlug(string slug);
    }
}