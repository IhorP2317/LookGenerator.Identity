
using LookGenerator.Persistence.Data.Configurations;
using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace LookGenerator.Persistence.Data ;

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<List<RoleSettings>> roleSettings, IOptions<AdminSettings> adminSettings )
       : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration(roleSettings.Value));
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration(adminSettings.Value));
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration(adminSettings.Value, roleSettings.Value.Find(r => r.Name == "Admin")! ));
        }
    }