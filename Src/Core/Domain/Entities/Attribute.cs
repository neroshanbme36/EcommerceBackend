using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Attribute : BaseEntity
    {
        public long Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Style {get; set;} = string.Empty;

        public IReadOnlyList<AttributeValue> AttributeValues {get; set;}
    }
}