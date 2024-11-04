using Application.Contracts.Features;
using Application.Contracts.Infrastructure.Epos;
using Application.Contracts.Persistence;
using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.CloudStoreEpos;

namespace Application.Features
{
    public class CartService : ICartService
    {
        private readonly IEposTransactionApiService _eposTransApiService;
        private readonly string _eposApiKey;
        private readonly string _appName;
        private readonly IMapper _mapper;
        private readonly IAppAccessTokenService _appAccessTokenService;
        private readonly IEposAccountApiService _eposAccountApiService;
        private readonly IUnitOfWork _uow;

        public CartService(IEposTransactionApiService eposTransApiService, IMapper mapper, IAppAccessTokenService appAccessTokenService, IEposAccountApiService eposAccountApiService, IUnitOfWork uow)
        {
            _eposTransApiService = eposTransApiService;
            _eposApiKey = "EcommDE1142";
            _appName = "Epos";
            _mapper = mapper;
            _appAccessTokenService = appAccessTokenService;
            _eposAccountApiService = eposAccountApiService;
            _uow = uow;
        }

        private async Task<bool> SaveUnCommitedChanges()
        {
            bool result = true;
            if (_uow.HasChanges()) 
                result = await _uow.Save() > 0;
            return result;
        }

        public async Task<OrderDto> GetCart(string deviceId, string cartId)
        {
            OrderDto? order = null;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_appName, _eposAccountApiService.GetAccessToken(_eposApiKey, deviceId));
            try
            {
                order = await _eposTransApiService.GetTransactionByGuid(appAccessToken.Token, _eposApiKey, deviceId, cartId);
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

        public async Task<OrderDto> AddOrEditCartHeader(string deviceId, CartHeaderInputDto cartHeaderInputDto)
        {
            OrderDto? order = null;
            EposTransHeaderInputDto request = _mapper.Map<EposTransHeaderInputDto>(cartHeaderInputDto);
            request.TransType = TransactionType.Sales;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_appName, _eposAccountApiService.GetAccessToken(_eposApiKey, deviceId));
            try
            {
                order = await _eposTransApiService.InsertOrUpdateHeader(appAccessToken.Token, _eposApiKey, deviceId, request);
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

        public async Task<ProductSearchResultDto> AddOrEditCartLine(string deviceId, CartLineInputDto cartLineInputDto)
        {
            ProductSearchResultDto? productSearchResultDto = null;
            EposTransLineInputDto request = _mapper.Map<EposTransLineInputDto>(cartLineInputDto);
            request.EntryType = EntryType.Product;
            request.TransType = TransactionType.Sales;
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_appName, _eposAccountApiService.GetAccessToken(_eposApiKey, deviceId));
            try
            {
                productSearchResultDto = await _eposTransApiService.InsertOrUpdateLine(appAccessToken.Token, _eposApiKey, deviceId, request);
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