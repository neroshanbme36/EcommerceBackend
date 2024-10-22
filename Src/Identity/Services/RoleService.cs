using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.Identity;
using Application.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityRoleDto?> GetRole(string id)
        {
            IdentityRole? identityRole = await _roleManager.Roles.FirstOrDefaultAsync(c => c.Id == id);
            if (identityRole == null) return null;
            return MapIdentityRole(identityRole);
        }

        public async Task<IdentityRoleDto?> GetRoleByName(string name)
        {
            IdentityRole? identityRole = await _roleManager.Roles.FirstOrDefaultAsync(c => c.Name == name);
            if (identityRole == null) return null;
            return MapIdentityRole(identityRole);
        }

        public async Task<IReadOnlyList<IdentityRoleDto>> GetRoles()
        {
            IReadOnlyList<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            List<IdentityRoleDto> identityRoleDtos = new List<IdentityRoleDto>();
            foreach(IdentityRole role in roles)
            {
                identityRoleDtos.Add(MapIdentityRole(role));
            }
            return identityRoleDtos;
        }

        private IdentityRoleDto MapIdentityRole(IdentityRole identityRole)
        {
            return new IdentityRoleDto
            {
                Id = identityRole.Id,
                Name = identityRole.Name
            };
        }
    }
}