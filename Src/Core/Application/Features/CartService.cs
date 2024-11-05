using Application.Contracts.Features;
using Application.Contracts.Infrastructure.Epos;
using Application.Contracts.Persistence;
using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Crm.Entities;
using Domain.Entities;
using Domain.Enums.CloudStoreEpos;
using Microsoft.Extensions.Options;

namespace Application.Features
{
    public class CartService : ICartService
    {
        private readonly IEposTransactionApiService _eposTransApiService;
        private readonly string _eposAppName;
        private readonly IMapper _mapper;
        private readonly IAppAccessTokenService _appAccessTokenService;
        private readonly IEposAccountApiService _eposAccountApiService;
        private readonly IUnitOfWork _uow;
        private readonly IDeviceService _deviceService;

        public CartService(IEposTransactionApiService eposTransApiService, IMapper mapper, IAppAccessTokenService appAccessTokenService, IEposAccountApiService eposAccountApiService, 
        IUnitOfWork uow, IDeviceService deviceService, IOptions<MicroservicesBaseUrl> microservicesBaseUrl)
        {
            _eposTransApiService = eposTransApiService;
            _eposAppName = microservicesBaseUrl.Value.EposAppName;
            _mapper = mapper;
            _appAccessTokenService = appAccessTokenService;
            _eposAccountApiService = eposAccountApiService;
            _uow = uow;
            _deviceService = deviceService;
        }

        private async Task<bool> SaveUnCommitedChanges()
        {
            bool result = true;
            if (_uow.HasChanges()) 
                result = await _uow.Save() > 0;
            return result;
        }

        public async Task<OrderDto> GetCart(string cartId)
        {
            Device device = await _deviceService.GetEcommerceDevice();
            OrderDto? order = null;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_eposAppName, _eposAccountApiService.GetAccessToken(device.ProductKey, device.Id));
            try
            {
                order = await _eposTransApiService.GetTransactionByGuid(appAccessToken.Token, device.ProductKey, device.Id, cartId);
            }
            catch (UnauthorizedException ex)
            {
                await _appAccessTokenService.MakeTokenExpire(appAccessToken);
                await SaveUnCommitedChanges();
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (NotFoundException)
            {
                await SaveUnCommitedChanges();
                throw new NotFoundException("Cart doesnt exist", cartId);
            }
            catch (InternalServerErrorException ex)
            {
                await SaveUnCommitedChanges();
                throw new InternalServerErrorException(ex.Message);
            }
            await SaveUnCommitedChanges();
            return order;
        }

        public async Task<OrderDto> AddOrEditCartHeader(CartHeaderInputDto cartHeaderInputDto)
        {
            Device device = await _deviceService.GetEcommerceDevice();
            OrderDto? order = null;
            EposTransHeaderInputDto request = _mapper.Map<EposTransHeaderInputDto>(cartHeaderInputDto);
            request.TransType = TransactionType.Sales;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_eposAppName, _eposAccountApiService.GetAccessToken(device.ProductKey, device.Id));
            try
            {
                order = await _eposTransApiService.InsertOrUpdateHeader(appAccessToken.Token, device.ProductKey, device.Id, request);
            }
            catch (UnauthorizedException ex)
            {
                await _appAccessTokenService.MakeTokenExpire(appAccessToken);
                await SaveUnCommitedChanges();
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                await SaveUnCommitedChanges();
                throw new InternalServerErrorException(ex.Message);
            }
            await SaveUnCommitedChanges();
            return order;
        }

        public async Task<ProductSearchResultDto> AddOrEditCartLine(CartLineInputDto cartLineInputDto)
        {
            Device device = await _deviceService.GetEcommerceDevice();
            ProductSearchResultDto? productSearchResultDto = null;
            EposTransLineInputDto request = _mapper.Map<EposTransLineInputDto>(cartLineInputDto);
            request.EntryType = EntryType.Product;
            request.TransType = TransactionType.Sales;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_eposAppName, _eposAccountApiService.GetAccessToken(device.ProductKey, device.Id));
            try
            {
                productSearchResultDto = await _eposTransApiService.InsertOrUpdateLine(appAccessToken.Token, device.ProductKey, device.Id, request);
            }
            catch (UnauthorizedException ex)
            {
                await _appAccessTokenService.MakeTokenExpire(appAccessToken);
                await SaveUnCommitedChanges();
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                await SaveUnCommitedChanges();
                throw new InternalServerErrorException(ex.Message);
            }
            await SaveUnCommitedChanges();
            return productSearchResultDto;
        }
    }
}