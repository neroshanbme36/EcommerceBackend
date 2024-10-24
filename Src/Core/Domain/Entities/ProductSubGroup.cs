using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class ProductSubGroup : BaseEntity
    {
        public string Id {get; set;} = string.Empty;
        public string? Description {get; set;}

        public string ProductGroupId {get; set;} = string.Empty;
        public ProductGroup? ProductGroup {get; set;}
        public IReadOnlyList<Product> Products {get; set;} = new List<Product>();
    }
}