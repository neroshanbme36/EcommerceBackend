using Application.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos.Payment
{
    public class PaymentResultDto
    {
        public PaymentResultCode Code { get; set; }
        public string? Message { get; set; }
        public OrderDto Order { get; set; }
    }
}