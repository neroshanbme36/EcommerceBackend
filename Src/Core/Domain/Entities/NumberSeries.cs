using Domain.Common;

namespace Domain.Entities
{
    public class NumberSeries : BaseEntity
    {
        public int Id {get; set;}
        public string DeviceId {get; set;} = string.Empty;
        public string TableName {get; set;} = string.Empty;
        public string? Prefix {get; set;}
        public long LastNoUsed {get; set;}
    }
}