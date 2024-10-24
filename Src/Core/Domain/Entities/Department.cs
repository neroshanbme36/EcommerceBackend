using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Id {get; set;} = string.Empty;
        public string? Description {get; set;}
        public bool AutoLabel {get; set;}
        public bool QuantityEntry {get; set;}
        public bool Commission {get; set;}
        public string? ParentId {get; set;}
        public string? LongDescription {get; set;}
        public bool ShowInEcommerce {get; set;}
        public bool ShowEcommHome {get; set;}
        public bool ShowEcommFooter {get; set;}
        
        public IReadOnlyList<Product> Products {get; set;}= new List<Product>();
        public IReadOnlyList<ProductDepartment> ProductDepartments {get; set;}
    }
}