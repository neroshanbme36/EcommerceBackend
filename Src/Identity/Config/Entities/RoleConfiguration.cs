using Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                    Name = RoleNames.ADMINISTRATOR,
                    NormalizedName = RoleNames.ADMINISTRATOR.ToUpper()
                },
                new IdentityRole
                {
                    Id = "ccc43a8e-f7bb-4445-baaf-1add431ffbbf",
                    Name = RoleNames.STAFF,
                    NormalizedName = RoleNames.STAFF.ToUpper()
                },
                new IdentityRole
                {
                    Id = "cdc43a8e-f7bb-4445-baaf-1add431ffbbf",
                    Name = RoleNames.MANAGER,
                    NormalizedName = RoleNames.MANAGER.ToUpper()
                }
            );
        }
    }
}