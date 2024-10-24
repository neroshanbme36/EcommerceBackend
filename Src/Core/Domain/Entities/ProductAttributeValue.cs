using Domain.Common;

namespace Domain.Entities
{
    public class ProductAttributeValue : BaseEntity
    {
        public string ItemNo {get; set;} = string.Empty;
        public Product? Product {get; set;}
        
        public long AttributeValueId {get; set;} 
        public AttributeValue? AttributeValue {get; set;}
    }
}