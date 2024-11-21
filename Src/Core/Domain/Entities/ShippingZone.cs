using Domain.Common;

namespace Domain.Entities
{
    public class ShippingZone : BaseEntity
    {
        public int Id {get; set;}
        public string ZoneName {get; set;} = string.Empty; //"Local","Regional","International"
        public decimal ShippingCost {get; set;}
        public decimal FreeShippingThreshold {get; set;}
        public string? CountryId {get; set;}
        public Country? country {get; set;}
        public IReadOnlyList<ShippingZonePostcode>? ShippingZonePostcodes {get; set;}
    }
}