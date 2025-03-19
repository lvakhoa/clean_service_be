using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

using StackExchange.Redis;

namespace CleanService.Src.Utils.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer redis, IDistributedCache cache, ILogger<RedisCacheService> logger)
    {
        _redis = redis;
        _cache = cache;
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (cachedValue == null)
                return default;

            return JsonSerializer.Deserialize<T>(cachedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving data from Redis cache");
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting data in Redis cache");
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing data from Redis cache");
        }
    }

    public async Task InvalidateByPrefixAsync(string keyPrefix)
    {
        if (string.IsNullOrEmpty(keyPrefix))
            throw new ArgumentException("Key prefix cannot be null or empty", nameof(keyPrefix));

        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());

        // Scan for keys matching the pattern
        var keys = server.Keys(pattern: $"*{keyPrefix}*");

        // Delete each matching key
        foreach (var key in keys)
        {
            await db.KeyDeleteAsync(key);
        }
    }
}
