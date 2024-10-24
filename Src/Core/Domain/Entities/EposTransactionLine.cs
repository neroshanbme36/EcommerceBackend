using Domain.Common;

namespace Domain.Entities.CloudStore
{
    public class EposTransactionLine : BaseOrderLine
    {
        public EposTransactionHeader EposTransactionHeader {get; set;}
    }
}