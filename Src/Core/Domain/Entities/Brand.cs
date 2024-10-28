using Domain.Common;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;}
        public bool ShowInEcommerce {get; set;}
    }
}