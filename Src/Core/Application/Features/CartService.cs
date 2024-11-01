using Application.Contracts.Features;
using Application.Contracts.Infrastructure.Epos;
using Application.Dtos.Cart;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Exceptions;
using AutoMapper;
using Domain.Enums.CloudStoreEpos;

namespace Application.Features
{
    public class CartService : ICartService
    {
        private readonly IEposTransactionApiService _eposTransApiService;
        private readonly string _eposApiKey;
        private readonly IMapper _mapper;

        public CartService(IEposTransactionApiService eposTransApiService, IMapper mapper)
        {
            _eposTransApiService = eposTransApiService;
            _eposApiKey = "Epos";
            _mapper = mapper;
        }

        public async Task<OrderDto> GetCart(string deviceId, string cartId)
        {
            OrderDto? order = await _eposTransApiService.GetTransactionByGuid(_eposApiKey, deviceId, cartId);
            if (order == null) throw new NotFoundException("Cart doesnt exist", cartId);
            return order;
        }

        public async Task<OrderDto> AddOrEditCartHeader(string deviceId, CartHeaderInputDto cartHeaderInputDto)
        {
            EposTransHeaderInputDto request = _mapper.Map<EposTransHeaderInputDto>(cartHeaderInputDto);
            request.TransType = TransactionType.Sales;
            return await _eposTransApiService.InsertOrUpdateHeader(_eposApiKey, deviceId, request);
        }

        public async Task<ProductSearchResultDto> AddOrEditCartLine(string deviceId, CartLineInputDto cartLineInputDto)
        {
            EposTransLineInputDto request = _mapper.Map<EposTransLineInputDto>(cartLineInputDto);
            request.EntryType = EntryType.Product;
            request.TransType = TransactionType.Sales;
            return await _eposTransApiService.InsertOrUpdateLine(_eposApiKey, deviceId, request);
        }
    }
}