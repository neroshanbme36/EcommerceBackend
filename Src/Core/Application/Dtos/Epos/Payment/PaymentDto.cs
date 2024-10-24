
using Application.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos.Payment
{
    public class PaymentDto
    {
        public TenderType TenderType {get; set;}
        public decimal Amount {get; set;}
        public long HeaderId {get; set;}
        public string? RepairId {get; set;}
    }
}