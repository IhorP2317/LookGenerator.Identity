using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LookGenerator.Persistence.Data.Configurations ;

    public class ApplicationUserConfiguration(AdminSettings adminSettings) :
        IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            var appUser = new ApplicationUser
            {
                Id = adminSettings.Id,
                Email = adminSettings.Email,
                NormalizedEmail = adminSettings.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = adminSettings.UserName,
                NormalizedUserName = adminSettings.UserName.ToUpper(),
                SecurityStamp = "7f6b6c59-b6a1-4f78-8d9c-3b02cf4706a6",
            };
            var ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, adminSettings.Password);
            builder.HasData(appUser);
        }
    }