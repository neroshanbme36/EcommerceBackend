using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class EposTransactionHeader : BaseOrderHeader
    {
        public IReadOnlyList<EposTransactionLine> EposTransactionLines {get; set;}
    }
}