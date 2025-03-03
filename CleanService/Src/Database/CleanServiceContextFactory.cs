using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanService.Src.Models;

internal class CleanServiceContextFactory : IDesignTimeDbContextFactory<CleanServiceContext>
{
    public CleanServiceContext CreateDbContext(string[] args)
    {
        var config = BuildConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<CleanServiceContext>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        
        optionsBuilder.UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString),
            mySqlOptions => { mySqlOptions.CommandTimeout(180); });

        return new CleanServiceContext(optionsBuilder.Options);
    }
    
    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}