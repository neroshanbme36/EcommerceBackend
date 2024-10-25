using Application.Dtos.Banner;
using Application.Dtos.Department;
using Application.Dtos.Product;

namespace Application.Dtos.Bootstrap
{
    public class HomePageResourceDto
    {
        public IReadOnlyList<HeroBannerDto> HeroBanners {get; set;} = new List<HeroBannerDto>();
        public IReadOnlyList<DepartmentProductMinifyDto> DepartmentProducts {get; set;} = new List<DepartmentProductMinifyDto>();
        public IReadOnlyList<ProductMinifyDto> FeaturedProducts {get; set;} = new List<ProductMinifyDto>();
        //Trending Now
        public IReadOnlyList<ProductMinifyDto> TrendingProducts {get; set;} = new List<ProductMinifyDto>();
        //Best Sellers
        public IReadOnlyList<ProductMinifyDto> TopSellingProducts {get; set;} = new List<ProductMinifyDto>();
        //Deals of the Day / Flash Sales
        public IReadOnlyList<ProductMinifyDto> TodayDealProducts {get; set;} = new List<ProductMinifyDto>();
        //New Arrivals
        public IReadOnlyList<ProductMinifyDto> RecentlyAddedProducts {get; set;} = new List<ProductMinifyDto>();
    }
}