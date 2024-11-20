using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Helpers;
using Application.Models;
using Application.QueryParams;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _storeUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;
        private readonly IMediaFileService _mediaFileService;

        public TransactionService(IUnitOfWork storeUnitOfWork, IMapper mapper, IDeviceService deviceService, IMediaFileService mediaFileService)
        {
            _storeUnitOfWork = storeUnitOfWork;
            _mapper = mapper;
            _deviceService = deviceService;
            _mediaFileService = mediaFileService;
        }

        public async Task<Pagination<OrderDto>> GetPostedTransactions(PostedTransHeaderParams headerParams, string userId)
        {
            string deviceId = await _deviceService.GetEcommerceDeviceId();
            List<long> ids = new List<long>();

            if (headerParams.Id.HasValue)
                ids.Add(headerParams.Id ?? 0);

            long totalCount = await GetPostedTransactionsCount(ids, deviceId, userId);

            var spec = new PostedTransactionHeaderSpecification(ids, true, deviceId, true,
            headerParams.PageSize, headerParams.PageNumber, headerParams.Sort, userId);

            IReadOnlyList<PostedTransactionHeader> headers = await _storeUnitOfWork.PostedTransactionHeaderRepository.GetList(spec);

            List<OrderDto> orderDtos = new List<OrderDto>();
            foreach (PostedTransactionHeader header in headers)
            {
                OrderDto orderDto = OrderHelper.MapToOrderDto(_mapper, header);
                orderDtos.Add(orderDto);
            }
            orderDtos = (await _mediaFileService.MapMediaFilesToOrders(orderDtos)).ToList();

            return new Pagination<OrderDto>(headerParams.PageNumber, headerParams.PageSize, totalCount, orderDtos);
        }

        private async Task<long> GetPostedTransactionsCount(IReadOnlyList<long> ids, string deviceId, string userId)
        {
            var spec = new PostedTransactionHeaderSpecification(ids, false, deviceId, true, null, null, null, userId);
            long count = await _storeUnitOfWork.PostedTransactionHeaderRepository.Count(spec);
            return count;
        }
    }
}