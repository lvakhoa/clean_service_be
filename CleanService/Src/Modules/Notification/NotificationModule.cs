using CleanService.Src.Modules.Notification.Infrastructures;
using CleanService.Src.Modules.Notification.Mapping.Profiles;
using CleanService.Src.Modules.Notification.Services;

namespace CleanService.Src.Modules.Notification;

public static class NotificationModule
{
    public static IServiceCollection AddNotificationMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(NotificationResponseProfile));

        return services;
    }
    
    public static IServiceCollection AddNotificationDependency(this IServiceCollection services)
    {
        services
            .AddScoped<INotificationService, FirebaseService>()
            .AddScoped<INotificationUnitOfWork, NotificationUnitOfWork>();
        
        return services;
    }

    public static IServiceCollection AddNotificationModule(this IServiceCollection services)
    {
        services
            .AddNotificationMapping()
            .AddNotificationDependency();

        return services;
    }
}