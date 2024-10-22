using CleanService.Src.Helpers;
using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using Microsoft.AspNetCore.Authentication;

namespace CleanService.Src.Modules;

public static class AppModule
{
    public static IServiceCollection AddAppDependency(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddSingleton<IConfiguration>(config)
            .AddTransient<IClaimsTransformation, ClaimsTransformation>()
            .AddAuthScheme(config)
            .AddAuthDependency()
            .AddNotificationModule(config)
            .AddMailModule(config);

        return services;
    }
}