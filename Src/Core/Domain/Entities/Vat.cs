using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Vat : BaseEntity
    {
        public string Id {get; set;} = string.Empty;
        public decimal Rate {get; set;}
        
        public IReadOnlyList<Product> Products {get; set;} = new List<Product>();
    }
}