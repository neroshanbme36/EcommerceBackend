using Domain.Common;

namespace Domain.Entities.CloudStore
{
    public class MediaFile : BaseEntity
    {
        public string Id {get; set;} = string.Empty;
        public string EntityType {get; set;} = string.Empty;
        public string? EntityId {get; set;}
        public string Type {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int Priority {get; set;}
        public string Extension {get; set;} = string.Empty;
        public string Path {get; set;} = string.Empty;
        public string Url {get; set;} = string.Empty;
    } 
}