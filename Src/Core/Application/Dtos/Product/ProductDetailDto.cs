using Application.Dtos.Tag;

namespace Application.Dtos.Product
{
    public class ProductDetailDto  : ProductMinifyDto
    {
        public decimal MaxOrderQuantity { get; set; } 
        public string? FullDescription {get; set;} // short description
        public string? LongDescription {get; set;}
        public string DepartmentId { get; set; } = string.Empty;
        public IReadOnlyList<TagDto> Tags {get; set;} = new List<TagDto>();
        public List<string> ImageUrls {get; set;} = new List<string>();
    }
}