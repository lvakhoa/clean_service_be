using CleanService.Src.Modules.Notification.Repositories;
using CleanService.Src.Modules.Notification.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Notification;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<INotificationService, FirebaseService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}