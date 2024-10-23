using CleanService.Src.Helpers;
using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using CleanService.Src.Modules.Booking;
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
            .AddBookingDependency()
            .AddAuthDependency()
            .AddNotificationModule(config)
            .AddMailModule(config);

        return services;
    }
}