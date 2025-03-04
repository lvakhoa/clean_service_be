using CleanService.Src.Models;

namespace CleanService.Src.Database;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(CleanServiceContext context)
    {


        await context.SaveChangesAsync();
    }
}
