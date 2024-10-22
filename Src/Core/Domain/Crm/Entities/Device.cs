using Domain.Common;

namespace Domain.Crm.Entities
{
    public class Device : BaseDomainEntity
    {
        public string Id {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        // 1 - Backoffice, 2 - Till
        public int Type {get; set;}
        public int LineNo {get; set;}
        public string IpAddress {get; set;} = string.Empty;
        public int Port {get; set;}
        public string Database {get; set;} = string.Empty;
        public string Username {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public string? EposMiddlewareId {get; set;}
        public string? ProductKey {get; set;}
        public bool IsActive {get; set;}
        public string? EndCode {get; set;}
        public string? DueCode {get; set;}
        public string StoreId {get; set;} = string.Empty;
        public Store? Store {get; set;}
    }
}