using Application.Dtos.Banner;
using Application.Dtos.Department;
using Application.Dtos.Product;

namespace Application.Dtos.Bootstrap
{
    public class HomePageResourceDto
    {
        public IReadOnlyList<HeroBannerDto> HeroBanners {get; set;} = new List<HeroBannerDto>();
        public IReadOnlyList<DepartmentProductMinifyDto> DepartmentProducts {get; set;} = new List<DepartmentProductMinifyDto>();
        public ProductHightlightsDto ProductHightlights {get; set;} = new ProductHightlightsDto();
    }
}