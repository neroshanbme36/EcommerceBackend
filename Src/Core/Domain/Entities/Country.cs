using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Id {get; set;}
        public string DisplayName {get; set;}

        //public IReadOnlyList<CloudStoreCustomer> Customers {get; set;} = new List<CloudStoreCustomer>();
        public IReadOnlyList<Store> Stores {get; set;} = new List<Store>();
        // public IReadOnlyList<CloudStoreUser> Users {get; set;} = new List<CloudStoreUser>();
        public IReadOnlyList<Vendor> Vendors {get; set;} = new List<Vendor>();
        public IReadOnlyList<CustomerAddress> CustomerAddresses {get; set;} = new List<CustomerAddress>();
        public IReadOnlyList<ShippingZone> ShippingZones {get; set;} = new List<ShippingZone>();
    }
}