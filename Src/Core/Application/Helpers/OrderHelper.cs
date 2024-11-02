using Application.Dtos.CloudStoreEpos.Epos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers
{
    public static class OrderHelper
    {
        public static OrderDto MapToOrderDto(IMapper mapper, PostedTransactionHeader header)
        {
            OrderHeaderDto orderHrDto = mapper.Map<OrderHeaderDto>(header);
            IReadOnlyList<OrderLineDto> orderLineDto = mapper.Map<IReadOnlyList<OrderLineDto>>(header.PostedTransactionLines);
            OrderDto dtoToReturn = new OrderDto(orderHrDto, orderLineDto);
            return dtoToReturn;
        }
    }
}