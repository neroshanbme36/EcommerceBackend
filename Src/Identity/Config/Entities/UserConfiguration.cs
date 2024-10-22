using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@yumarone.com",
                     NormalizedEmail = "ADMIN@YUMARONE.COM",
                     FirstName = "System",
                     LastName = "Admin",
                     UserName = "admin@yumarone.com",
                     NormalizedUserName = "ADMIN@YUMARONE.COM",
                     PasswordHash = hasher.HashPassword(null, "Password@123"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}