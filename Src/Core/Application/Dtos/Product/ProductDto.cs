
namespace Application.Dtos.Product
{
    public class ProductDto : ProductMinifyDto
    {
        public bool IsFeatured { get; set; }
        public bool IsTrending { get; set; }
        public bool IsTopSelling { get; set; }
        public bool IsTodayDeal { get; set; }
        public bool IsRecentlyAdded { get; set; }
        public string DepartmentId { get; set; } = string.Empty;
        public List<string> ImageUrls {get; set;} = new List<string>();
    }
}