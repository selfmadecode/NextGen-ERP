using Auth.Models;
using Auth.Ultilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Helper.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public PasswordHasher<ApplicationUser> Hasher { get; set; } = new PasswordHasher<ApplicationUser>();

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            SetupUsers(builder);
        }
        private void SetupUsers(EntityTypeBuilder<ApplicationUser> builder)
        {
            var admin = new ApplicationUser
            {
                FirstName = "Nextgen",
                LastName = "Admin",
                Id = Defaults.AdminId,
                Email = Defaults.AdminEmail,
                EmailConfirmed = true,
                NormalizedEmail = Defaults.AdminEmail.ToUpper(),
                UserName = Defaults.AdminEmail,
                NormalizedUserName = Defaults.AdminEmail.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "nextGen_1"),
                SecurityStamp = "99ae0c45-d682-4542-9ba7-1281e471916b",
            };

            var sysUser = new ApplicationUser
            {
                FirstName = "Nextgen",
                LastName = "User",
                Id = Defaults.SysUserId,
                Email = Defaults.SysUserEmail,
                EmailConfirmed = true,
                NormalizedEmail = Defaults.SysUserEmail.ToUpper(),
                UserName = Defaults.SysUserEmail,
                NormalizedUserName = Defaults.SysUserEmail.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "nextGen_1"),
                SecurityStamp = "99ae0c45-d682-4542-9ba7-1281e471916b",
            };

            builder.HasData(sysUser, admin);
        }
    }
}
