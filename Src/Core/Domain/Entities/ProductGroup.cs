
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class ProductGroup : BaseEntity
    {
        public string Id { get; set; } = string.Empty;
        public string? Description { get; set; }

        public IReadOnlyList<ProductSubGroup> ProductSubGroups { get; set; } = new List<ProductSubGroup>();
        public IReadOnlyList<Product> Products { get; set; } = new List<Product>();
    }
}