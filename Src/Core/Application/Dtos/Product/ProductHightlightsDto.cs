
namespace Application.Dtos.Product
{
    public class ProductHightlightsDto
    {
        public List<ProductMinifyDto> FeaturedProducts {get; set;} = new List<ProductMinifyDto>();
        //Trending Now
        public List<ProductMinifyDto> TrendingProducts {get; set;} = new List<ProductMinifyDto>();
        //Best Sellers
        public List<ProductMinifyDto> TopSellingProducts {get; set;} = new List<ProductMinifyDto>();
        //Deals of the Day / Flash Sales
        public List<ProductMinifyDto> TodayDealProducts {get; set;} = new List<ProductMinifyDto>();
        //New Arrivals
        public List<ProductMinifyDto> RecentlyAddedProducts {get; set;} = new List<ProductMinifyDto>();
    }
}