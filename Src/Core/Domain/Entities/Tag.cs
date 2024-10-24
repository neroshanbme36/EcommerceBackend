using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Tag : BaseEntity
    {
        public long Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;}

        public IReadOnlyList<ProductTag> ProductTags {get; set;}
    }
}