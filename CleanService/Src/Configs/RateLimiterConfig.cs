using System.Threading.RateLimiting;

using CleanService.Src.Constant;

namespace CleanService.Src.Configs;

public static class RateLimiterConfig
{
    public static void RegisterRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        Window = TimeSpan.FromSeconds(10)
                    }));

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    StatusCode = 429,
                    Message = "Too many requests. Please try again later.",
                    ExceptionCode = ExceptionConvention.RateLimitExceeded,
                }, token);
            };
        });
    }
}
