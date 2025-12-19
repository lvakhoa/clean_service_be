using CleanService.Src.Constant;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CleanService.Src.Utils;

public enum ClientCategory
{
    Unknown,
    Web,
    MobileApp
}

public class HttpClientHandler
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private static readonly List<string> CustomMobileAppIndicators = ["app", "mobile"];

    private static readonly List<string> CustomWebIndicators = ["web", "browser"];

    private static readonly List<string> MobileAppIndicators =
    [
        "okhttp", "dalvik", "android", "cfnetwork", "iphone", "ipad"
    ];

    private static readonly List<string> WebIndicators = ["mozilla", "chrome", "safari", "edge", "firefox"];

    public HttpClientHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public ClientCategory GetClientCategory()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.Request == null) return ClientCategory.Unknown;

        var req = context.Request;

        if (req.Headers.TryGetValue(HttpConstants.CustomClientTypeHeader, out var clientTypeHeader))
        {
            return GetClientCategory(clientTypeHeader.ToString());
        }

        return ClientCategory.Unknown;
    }

    public ClientCategory GetClientCategory(string? clientTypeHeader)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.Request == null) return ClientCategory.Unknown;

        var req = context.Request;

        // Prefer explicit header set by the client (recommended)
        if (!string.IsNullOrEmpty(clientTypeHeader))
        {
            var v = clientTypeHeader.ToLowerInvariant();
            if (CustomMobileAppIndicators.Contains(v)) return ClientCategory.MobileApp;
            if (CustomWebIndicators.Contains(v)) return ClientCategory.Web;
        }

        // Fallback to User-Agent sniffing
        if (!req.Headers.TryGetValue("User-Agent", out var ua)) return ClientCategory.Unknown;

        var userAgent = ua.ToString().ToLowerInvariant();
        // common app identifiers
        if (MobileAppIndicators.Contains(userAgent))
        {
            return ClientCategory.MobileApp;
        }

        // basic browser indicators
        if (WebIndicators.Contains(userAgent))
        {
            return ClientCategory.Web;
        }

        return ClientCategory.Unknown;
    }

    public string getClientUrl()
    {
        var clientCategory = GetClientCategory();
        return clientCategory switch
        {
            ClientCategory.MobileApp => _configuration["APP_URL"] ?? string.Empty,
            ClientCategory.Web => _configuration["WEB_URL"] ?? string.Empty,
            _ => _configuration["API_URL"] ?? string.Empty
        };
    }
}
