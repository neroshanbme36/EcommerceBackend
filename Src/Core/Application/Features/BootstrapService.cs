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

        public async Task<BootstrapResponseDto> GetBootstrapDatas(string deviceId, string userEmail, BootstrapRequestDto bootstrapRequestDto)
        {
            BootstrapResponseDto bootstrapResponseDto = new BootstrapResponseDto();
            bootstrapResponseDto.Store = await _storeService.GetStore();

            if (!string.IsNullOrWhiteSpace(userEmail))
                bootstrapResponseDto.User = await _authService.GetUserByEmail(userEmail);

            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                bootstrapResponseDto.Configuration = await _configurationService.GetConfigAttributeValue(deviceId);

                if (!string.IsNullOrWhiteSpace(bootstrapRequestDto.CartId))
                    bootstrapResponseDto.Order = await GetOrderFromEposApi(deviceId, bootstrapRequestDto.CartId);
            }

            bootstrapResponseDto.Departments = await _departmentService.GetDepartments();

            return bootstrapResponseDto;
        }

        private async Task<OrderDto?> GetOrderFromEposApi(string deviceId, string repairId)
        {
            OrderDto? order = await _eposTransApiService.GetRepairTransaction(_eposApiKey, deviceId, repairId);
            if (order != null) return order;
            return await _postedTransactionApiService.GetRepairTransaction(_eposApiKey, deviceId, repairId);
        }
    }
}