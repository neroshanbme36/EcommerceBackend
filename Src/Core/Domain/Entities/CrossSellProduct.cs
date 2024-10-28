using Domain.Common;

namespace Domain.Entities
{
    public class CrossSellProduct : BaseEntity
    {
        public string ItemNo { get; set; } = string.Empty;
        public string CrossSellItemNo { get; set; }  = string.Empty;
    }
}