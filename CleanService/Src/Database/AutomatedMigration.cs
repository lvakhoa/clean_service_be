using CleanService.Src.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Database;

public static class AutomatedMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<CleanServiceContext>();

        if (context.Database.IsMySql()) await context.Database.MigrateAsync();

        await DatabaseContextSeed.SeedDatabaseAsync(context);
    }
}
