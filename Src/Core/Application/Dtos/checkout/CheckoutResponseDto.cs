using Application.Dtos.CloudStoreEpos.Epos;
using Application.Enums;

namespace Application.Dtos.checkout
{
    public class CheckoutResponseDto
    {
        public CheckoutResultCode Code {get; set;}
        public string? Message {get; set;}
        public OrderDto? Order { get; set; }

        public CheckoutResponseDto()
        {

        }

        public CheckoutResponseDto(string message)
        {
            Code = CheckoutResultCode.Error;
            Message = message;
        }
    }
}