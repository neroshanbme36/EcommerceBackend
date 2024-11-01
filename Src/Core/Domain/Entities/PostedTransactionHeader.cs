using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class PostedTransactionHeader : BaseOrderHeader
    {
        public string ZReportId {get; set;}
        public string ShiftReportId {get; set;}
        public bool Printed {get; set;}
        public long EcommOrderId {get; set;}
        public IReadOnlyList<PostedTransactionLine> PostedTransactionLines {get; set;}
    }
}