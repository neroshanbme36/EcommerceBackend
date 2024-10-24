using Domain.Common;

namespace Domain.Entities
{
    public class BarcodeMask : BaseEntity
    {
        public string Prefix {get; set;} = string.Empty;
        public string Mask {get; set;} = string.Empty;
        public int PrefixLength {get; set;}
        
        public string ItemNo { get; set; } = string.Empty;
        public Product? Product {get; set;}
    }
}