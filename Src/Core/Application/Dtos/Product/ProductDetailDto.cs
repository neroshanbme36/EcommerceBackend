using Application.Dtos.Department;
using Application.Dtos.Tag;

namespace Application.Dtos.Product
{
    public class ProductDetailDto  : ProductMinifyDto
    {
        public decimal MaxOrderQuantity { get; set; } 
        public string? FullDescription {get; set;} // short description
        public string? LongDescription {get; set;}
        public List<string> ImageUrls {get; set;} = new List<string>();
        public IReadOnlyList<DepartmentMinifyDto> Departments {get; set;} = new List<DepartmentMinifyDto>();
        public IReadOnlyList<TagDto> Tags {get; set;} = new List<TagDto>();
        public IReadOnlyList<ProductMinifyDto> RelatedProducts {get; set;} = new List<ProductMinifyDto>();
    }
}