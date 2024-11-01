using Application.Contracts.Features;
using Application.Contracts.Identity;
using Application.Dtos.Bootstrap;
using Application.Dtos.CloudStoreEpos.Epos;

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

        public BootstrapService(IStoreService storeService, IConfigurationService configurationService,
        IDepartmentService departmentService, IAuthService authService,
        IBannerService bannerService, IProductService productService,
        IAttributeService attributeService, ICountryService countryService, ICartService cartService)
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
        }

        public async Task<PrimeBaseResponseDto> GetPrimeBase(string deviceId, string userEmail, PrimeBaseRequestDto request)
        {
            PrimeBaseResponseDto response = new PrimeBaseResponseDto();
            response.Store = await _storeService.GetStore();

            if (!string.IsNullOrWhiteSpace(userEmail))
                response.User = await _authService.GetUserByEmail(userEmail);

            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                response.Configuration = await _configurationService.GetConfigAttributeValue(deviceId);

                if (!string.IsNullOrWhiteSpace(request.CartId))
                {
                    try
                    {
                        response.Order = await _cartService.GetCart(deviceId, request.CartId);
                    }
                    catch {}
                }
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