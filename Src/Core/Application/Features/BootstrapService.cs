using Application.Contracts.Features;
using Application.Contracts.Identity;
using Application.Contracts.Infrastructure.Epos;
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
        private readonly IEposTransactionApiService _eposTransApiService;
        private readonly IPostedTransactionApiService _postedTransactionApiService;
        private readonly string _eposApiKey;

        public BootstrapService(IStoreService storeService, IConfigurationService configurationService,
        IDepartmentService departmentService, IAuthService authService, IEposTransactionApiService eposTransApiService,
        IPostedTransactionApiService postedTransactionApiService)
        {
            _storeService = storeService;
            _configurationService = configurationService;
            _departmentService = departmentService;
            _authService = authService;
            _eposTransApiService = eposTransApiService;
            _postedTransactionApiService = postedTransactionApiService;
            _eposApiKey = "Epos";
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
                    response.Order = await GetOrderFromEposApi(deviceId, request.CartId);
            }

            response.Departments = await _departmentService.GetDepartments();

            return response;
        }

        private async Task<OrderDto?> GetOrderFromEposApi(string deviceId, string repairId)
        {
            OrderDto? order = await _eposTransApiService.GetRepairTransaction(_eposApiKey, deviceId, repairId);
            if (order != null) return order;
            return await _postedTransactionApiService.GetRepairTransaction(_eposApiKey, deviceId, repairId);
        }
    }
}