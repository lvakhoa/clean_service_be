using CleanService.Src.Utils.Cache;

namespace CleanService.Src.Configs;

public static class CachingConfig
{
    public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "CleanService_";
        });

        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton(typeof(PaginatedCacheService<,>));
    }
}
