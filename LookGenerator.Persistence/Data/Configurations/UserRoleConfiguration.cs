using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookGenerator.Persistence.Data.Configurations ;

    public class UserRoleConfiguration(AdminSettings adminSettings, RoleSettings roleSettings):IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(new IdentityUserRole<Guid>
            {
                UserId =  adminSettings.Id,
                RoleId = roleSettings.Id
            });
        }
    }