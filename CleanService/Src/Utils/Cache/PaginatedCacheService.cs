using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Utils.Cache;

public class PaginatedCacheService<T, TD> where T : Pagination<TD>, new() where TD : class
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<PaginatedCacheService<T, TD>> _logger;

    public PaginatedCacheService(ICacheService cacheService, ILogger<PaginatedCacheService<T, TD>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    /// <summary>
    /// Gets or sets cached data for paginated results with efficient memory usage
    /// </summary>
    public async Task<Pagination<TD>> GetPaginatedDataAsync(
        string baseKey,
        int? page,
        int? pageSize,
        Func<int?, int?, Task<T>> dataFetchFunc,
        TimeSpan cacheDuration)
    {
        // Create cache keys for this specific request
        string pageKey = $"{baseKey}:p{page}:s{pageSize}";
        string countKey = $"{baseKey}:count";

        // Try to get the current page from cache
        var cachedItems = await _cacheService.GetAsync<List<TD>>(pageKey);
        var cachedTotalCount = await _cacheService.GetAsync<int?>(countKey);

        // If we don't have the data cached, fetch from the database
        if (cachedItems == null || cachedTotalCount == null)
        {
            _logger.LogInformation("Cache miss for {pageKey}", pageKey);

            // Fetch data from data source
            var pagination = (await dataFetchFunc(page, pageSize)) as Pagination<TD>;

            // Cache the results
            await _cacheService.SetAsync(pageKey, pagination.Results, cacheDuration);
            await _cacheService.SetAsync(countKey, pagination.TotalItems, cacheDuration);

            return new Pagination<TD>
            {
                Results = pagination.Results,
                CurrentPage = page ?? 1,
                TotalItems = pagination.TotalItems,
                TotalPages = (int) Math.Ceiling(pagination.TotalItems / (double) (pageSize ?? pagination.TotalItems))
            };
        }

        _logger.LogInformation("Cache hit for {pageKey}", pageKey);

        // Return cached data
        return new Pagination<TD>(cachedItems, cachedTotalCount.Value, page ?? 1, pageSize ?? cachedTotalCount.Value);
    }

    /// <summary>
    /// Invalidates all pagination cache for a specific entity type
    /// </summary>
    public async Task InvalidatePaginationCacheAsync(string baseKey)
    {
        // Use Redis scan command to find all keys with this prefix
        // Here we're using a specialized method that would need to be implemented in ICacheService
        await _cacheService.InvalidateByPrefixAsync($"{baseKey}:");
    }
}
