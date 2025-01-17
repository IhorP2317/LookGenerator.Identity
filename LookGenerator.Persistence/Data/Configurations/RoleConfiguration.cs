using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LookGenerator.Persistence.Data.Configurations ;

    public class RoleConfiguration(List<RoleSettings> roleSettings ) : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.Property(r => r.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            builder.HasData(
                roleSettings.Select(role => new IdentityRole<Guid>
                {
                    Id = role.Id,
                    Name = role.Name,
                    NormalizedName = role.Name.ToUpper(),
                    ConcurrencyStamp = role.ConcurrencyStamp
                }).ToArray());
        }
    }