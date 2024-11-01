using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Booking;
using CleanService.Src.Modules.Contract;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using CleanService.Src.Modules.Service;
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
        
        // Inject Auth Module
        services
            .AddAuthModule(config);
            
        // Inject Notification Module
        services
            .AddNotificationModule(config);
        
        // Inject Mail Module
        services
            .AddMailModule(config);
        
        // Inject Booking Module
        services
            .AddBookingModule();
        
        // Inject Service Module
        services
            .AddServiceModule();
        
        // Inject Contract Module
        services
            .AddContractModule();

        return services;
    }
}