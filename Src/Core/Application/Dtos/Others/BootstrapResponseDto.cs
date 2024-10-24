using Application.Dtos.CloudStoreEpos.Epos;
using Application.Dtos.Configuration;
using Application.Dtos.Department;
using Application.Dtos.Identity;
using Application.Dtos.Store;

namespace Application.Dtos.Others
{
    public class BootstrapResponseDto
    {
        public StoreDto? Store {get; set;}
        public UserDto? User {get; set;}
        public ConfigAttributeValueDto? Configuration {get; set;}
        public IReadOnlyList<DepartmentDto> DepartmentDtos {get; set;} = new List<DepartmentDto>();
        public OrderDto? Order {get; set;}
    }
}