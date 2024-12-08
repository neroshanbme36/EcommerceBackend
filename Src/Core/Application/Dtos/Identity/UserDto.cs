using Application.Dtos.WishlistProduct;

namespace Application.Dtos.Identity
{
    public class UserDto
    {
        public string Id {get; set;} = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName {get; set;} = string.Empty;
        public string PhoneNumber {get; set;} = string.Empty;
        public string RoleName {get; set;} = string.Empty;
        public IReadOnlyList<WishlistProductDto> WishlistProducts {get; set;} = new List<WishlistProductDto>();
    }
}