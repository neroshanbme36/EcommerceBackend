using Domain.Common;

namespace Domain.Entities
{
    public class PostedTransactionLine : BaseOrderLine
    {
        public string ZReportId {get; set;}
        public PostedTransactionHeader PostedTransactionHeader {get; set;}
    }
}