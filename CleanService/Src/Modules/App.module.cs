using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Booking;
using CleanService.Src.Modules.Contract;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using CleanService.Src.Modules.Scheduler;
using CleanService.Src.Modules.ServiceType;
using CleanService.Src.Repositories;
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.BookingDetail;
using CleanService.Src.Repositories.Complaint;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.Helper;
using CleanService.Src.Repositories.Notification;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceCategory;
using CleanService.Src.Repositories.ServiceType;
using CleanService.Src.Repositories.User;
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
        
        // Inject Base Repository
        services
            .AddRepositories();
        
        // Inject Auth Module
        services
            .AddAuthModule(config);
            
        // Inject Notification Module
        services
            .AddNotificationModule();
        
        // Inject Mail Module
        services
            .AddMailModule(config);
        
        // Inject Booking Module
        services
            .AddBookingModule();
        
        // Inject Booking Module
        services
            .AddSchedulerModule();
        
        // Inject Service Module
        services
            .AddServiceTypeModule();
        
        // Inject Contract Module
        services
            .AddContractModule();
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
            .AddScoped<IBookingRepository, BookingRepository>()
            .AddScoped<IBookingDetailRepository, BookingDetailRepository>()
            .AddScoped<IComplaintRepository, ComplaintRepository>()
            .AddScoped<IContractRepository, ContractRepository>()
            .AddScoped<IDurationPriceRepository, DurationPriceRepository>()
            .AddScoped<IHelperRepository, HelperRepository>()
            .AddScoped<INotificationRepository, NotificationRepository>()
            .AddScoped<IRoomPricingRepository, RoomPricingRepository>()
            .AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>()
            .AddScoped<IServiceTypeRepository, ServiceTypeRepository>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}