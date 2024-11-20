using Application.Dtos.CloudStoreEpos.Epos;

namespace Application.Contracts.Features
{
    public interface IMediaFileService
    {
        Task<OrderDto> MapMediaFileToOrder(OrderDto order);
        Task<IReadOnlyList<OrderDto>> MapMediaFilesToOrders(IReadOnlyList<OrderDto> orderDtos);
    }
}