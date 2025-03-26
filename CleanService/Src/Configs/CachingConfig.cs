using CleanService.Src.Utils.Cache;

using StackExchange.Redis;

namespace CleanService.Src.Configs;

public static class CachingConfig
{
    public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var redisHost = configuration.GetValue<string>("Redis:Host") ?? "localhost";
        var redisPort = configuration.GetValue<int>("Redis:Port");
        var redisPassword = configuration.GetValue<string>("Redis:Password");
        var redisUser = configuration.GetValue<string>("Redis:User") ?? "default";
        var config = new ConfigurationOptions
        {
            EndPoints = { { redisHost, redisPort } },
            User = redisUser,
            Password = redisPassword
        };

        try
        {
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(config));
        }
        catch (Exception ex)
        {
            throw new Exception("Error connecting to Redis", ex);
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.ToString();
            options.InstanceName = "CleanService_";
        });

        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped(typeof(PaginatedCacheService<,>));
    }
}
