using LookGenerator.Persistence.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LookGenerator.Persistence.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await applicationContext.Database.MigrateAsync();
    }
}