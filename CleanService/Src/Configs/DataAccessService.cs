using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Repositories.Impl;
using CleanService.Src.Models;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Configs;

public static class DataAccessService
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddInfrastructure();

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: {connectionString}");

        services.AddDbContext<CleanServiceContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
    }
}
