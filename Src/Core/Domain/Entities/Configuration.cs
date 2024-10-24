using Domain.Common;

namespace Domain.Entities
{
    public class Configuration : BaseEntity
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Detail { get; set; }
        public string Comments { get; set; }
        public string ParentCode {get; set;}
        public string CodeType {get; set;}
        public bool IsReadOnly {get; set;}
        public bool ShowInUi {get; set;}
    }
}