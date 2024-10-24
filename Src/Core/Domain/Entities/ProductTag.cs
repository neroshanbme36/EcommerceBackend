using Domain.Common;

namespace Domain.Entities
{
    public class ProductTag : BaseEntity
    {
        public string ItemNo { get; set; }  = string.Empty;
        public Product? Product {get; set;}

        public long TagId {get; set;} 
        public Tag? Tag {get; set;} 
    }
}