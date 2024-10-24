using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class AttributeValue : BaseEntity
    {
        public long Id {get; set;}
        public string Value {get; set;} = string.Empty;
        public string? HexColor {get; set;}

        public long AttributeId {get; set;}
        public Attribute? Attribute {get; set;}

        public IReadOnlyList<ProductAttributeValue> ProductAttributeValues {get; set;}
    }
}