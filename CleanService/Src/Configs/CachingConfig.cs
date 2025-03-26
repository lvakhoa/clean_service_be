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
            Password = redisPassword,
            ConnectTimeout = 5000,
            SyncTimeout = 5000,
            AsyncTimeout = 5000,
            AllowAdmin = true,
            Ssl = false
        };

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var connection = ConnectionMultiplexer.Connect(config);

            return connection;
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.ToString();
            options.InstanceName = "CleanService_";
        });

        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped(typeof(PaginatedCacheService<,>));
    }
}
