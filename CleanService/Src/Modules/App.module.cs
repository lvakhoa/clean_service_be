using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Authentication;

namespace CleanService.Src.Modules;

public static class AppModule
{
    public static IServiceCollection AddAppDependency(this IServiceCollection services, IConfiguration config)
    {
        // Inject Configuration
        services
            .AddSingleton<IConfiguration>(config);
        
        // Inject Auth Services
        services
            .AddTransient<IClaimsTransformation, ClaimsTransformation>()
            .AddAuthScheme(config)
            .AddAuthDependency()
            .AddAuthMapping();
            
        // Inject Notification Services
        services
            .AddNotificationModule(config);
        
        // Inject Mail Services
        services
            .AddMailModule(config);

        return services;
    }
}