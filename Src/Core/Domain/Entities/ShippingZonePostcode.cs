using Domain.Common;

namespace Domain.Entities
{
    public class ShippingZonePostcode : BaseEntity
    {
        public int Id {get; set;}
        public string? Postcode {get; set;}
        public string? PostcodePattern { get; set; } 
        public string? PostcodeRangeStart { get; set; }
        public string? PostcodeRangeEnd { get; set; }
        public int ShippingZoneId {get; set;}
        public ShippingZone? ShippingZone {get; set;}
    }
}