using Application.Abstractions;
using LookGenerator.Persistence.Data;
using LookGenerator.Persistence.Services;
using LookGenerator.Persistence.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LookGenerator.Persistence.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<List<RoleSettings>>(configuration.GetSection("RoleSettings"))
            .Configure<AdminSettings>(configuration.GetSection("AdminSettings"))
            .Configure<AuthSettings>(configuration.GetSection("AuthSettings"))
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                        npgsqlOptions =>
                        {
                            npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        })
                    .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
            })
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITokenGenerator, TokenGenerator>()
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}