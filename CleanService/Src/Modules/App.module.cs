using CleanService.Src.Helpers;
using CleanService.Src.Modules.Auth;
using Microsoft.AspNetCore.Authentication;

namespace CleanService.Src.Modules;

public static class AppModule
{
    public static IServiceCollection AddAppDependency(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddTransient<IClaimsTransformation, ClaimsTransformation>()
            .AddAuthScheme(config)
            .AddAuthDependency();

        return services;
    }
}