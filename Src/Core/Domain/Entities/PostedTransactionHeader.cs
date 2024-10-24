using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities.CloudStore
{
    public class PostedTransactionHeader : BaseOrderHeader
    {
        public string ZReportId {get; set;}
        public string ShiftReportId {get; set;}
        public bool Printed {get; set;}
        public IReadOnlyList<PostedTransactionLine> PostedTransactionLines {get; set;}
    }
}