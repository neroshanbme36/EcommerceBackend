using Domain.Common;

namespace Domain.Entities
{
    public class RelatedProduct : BaseEntity
    {
        public string ItemNo { get; set; }  = string.Empty;
        public string RelatedProductItemNo { get; set; }  = string.Empty;
    }
}