using Domain.Common;

namespace Domain.Entities
{
    public class WishlistProduct : BaseEntity
    {
        public string UserId {get; set;} = string.Empty;
        public string ItemNo {get; set;} = string.Empty;
        public DateTime CreatedOn {get; set;} = Convert.ToDateTime("1900-01-01");

        public Product Product {get; set;}
    }
}