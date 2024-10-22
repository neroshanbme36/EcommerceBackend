using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Identity;

namespace Application.Contracts.Identity
{
    public interface IRoleService
    {
        Task<IdentityRoleDto?> GetRole(string id);
        Task<IdentityRoleDto?> GetRoleByName(string name);
        Task<IReadOnlyList<IdentityRoleDto>> GetRoles();
    }
}