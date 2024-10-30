using Domain.Common;

namespace Domain.Entities
{
    public class CustomerAddress : BaseAddressDomainEposEntity
    {
        public long Id {get; set;}
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone {get; set;}
        public string Category {get; set;} = string.Empty;
        public string Type {get; set;}  = string.Empty;
        public bool IsDefault {get; set;}
        public string? EcommUserId {get; set;}
        public string? CustomerId {get; set;}

        public Country? Country { get; set; }
    }
}