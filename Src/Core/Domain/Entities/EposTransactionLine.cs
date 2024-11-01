using Domain.Common;

namespace Domain.Entities
{
    public class EposTransactionLine : BaseOrderLine
    {
        public EposTransactionHeader EposTransactionHeader {get; set;}
    }
}