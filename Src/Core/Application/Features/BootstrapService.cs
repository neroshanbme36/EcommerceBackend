using Application.Contracts.Features;
using Application.Contracts.Identity;
using Application.Dtos.Bootstrap;

namespace Application.Features
{
    public class BootstrapService : IBootstrapService
    {
        private readonly IStoreService _storeService;
        private readonly IConfigurationService _configurationService;
        private readonly IDepartmentService _departmentService;
        private readonly IAuthService _authService;
        private readonly IBannerService _bannerService;
        private readonly IProductService _productService;
        private readonly IAttributeService _attributeService;
        private readonly ICountryService _countryService;
        private readonly ICartService _cartService;
        private readonly IWishlistProductService _wishlistProductService;

        public BootstrapService(IStoreService storeService, IConfigurationService configurationService,
        IDepartmentService departmentService, IAuthService authService,
        IBannerService bannerService, IProductService productService,
        IAttributeService attributeService, ICountryService countryService, ICartService cartService,
        IWishlistProductService wishlistProductService)
        {
            _storeService = storeService;
            _configurationService = configurationService;
            _departmentService = departmentService;
            _authService = authService;
            _bannerService = bannerService;
            _productService = productService;
            _attributeService = attributeService;
            _countryService = countryService;
            _cartService = cartService;
            _wishlistProductService = wishlistProductService;
        }

        public async Task<PrimeBaseResponseDto> GetPrimeBase(string userEmail, PrimeBaseRequestDto request)
        {
            PrimeBaseResponseDto response = new PrimeBaseResponseDto();
            response.Store = await _storeService.GetStore();

            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                try {
                    response.User = await _authService.GetUserByEmail(userEmail);
                } catch {}
                if (response.User != null)
                {
                    response.User.WishlistProducts = await _wishlistProductService.GetWishlistProductsByUserId(response.User.Id);
                }
            }

            response.Configuration = await _configurationService.GetConfigAttributeValue();

            if (!string.IsNullOrWhiteSpace(request.CartId))
            {
                try
                {
                    response.Order = await _cartService.GetCart(request.CartId);
                }
                catch { }
            }

            response.Departments = await _departmentService.GetDepartments();
            response.Countries = await _countryService.GetCountries();

            return response;
        }

        public async Task<HomePageResourceDto> GetHomePageResource()
        {
            HomePageResourceDto response = new HomePageResourceDto();
            response.HeroBanners = await _bannerService.GetHeroBanners();
            response.DepartmentProducts = await _productService.GetHomepageDepartmentProducts();
            response.ProductHightlights = await _productService.GetProductHighlights();
            return response;
        }

        public async Task<BrowseCategoryPageResourceDto> GetBrowseCategoryPageResource()
        {
            BrowseCategoryPageResourceDto response = new BrowseCategoryPageResourceDto();
            response.AttributeValues = await _attributeService.GetAttributeValues();
            return response;
        }
    }
}