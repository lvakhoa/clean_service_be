using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Booking;
using CleanService.Src.Modules.Contract;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Notification;
using CleanService.Src.Modules.Payment;
using CleanService.Src.Modules.Scheduler;
using CleanService.Src.Modules.ServiceType;
using CleanService.Src.Repositories;
using CleanService.Src.Repositories.BlacklistedUser;
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.BookingContract;
using CleanService.Src.Repositories.BookingDetail;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.Feedback;
using CleanService.Src.Repositories.Helper;
using CleanService.Src.Repositories.HelperAvailabilities;
using CleanService.Src.Repositories.Notification;
using CleanService.Src.Repositories.Refund;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceCategory;
using CleanService.Src.Repositories.ServiceType;
using CleanService.Src.Repositories.User;
using CleanService.Src.Utils;
using CleanService.Src.Utils.RequestClient;
using Microsoft.AspNetCore.Authentication;

namespace CleanService.Src.Modules;

public static class AppModule
{
    public static IServiceCollection AddAppDependency(this IServiceCollection services, IConfiguration config)
    {
        // Inject Configuration
        services
            .AddSingleton<IConfiguration>(config);

        services
            .AddTransient<IRequestClient, RequestClient>();
        
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
        
        // Inject Service Module
        services
            .AddServiceTypeModule();
        
        // Inject Contract Module
        services
            .AddContractModule();
        
        // Inject Payment Module
        services
            .AddPaymentModule();
        
        // Inject Scheduler Module
        services
            .AddSchedulerModule();
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
            .AddScoped<IBlacklistedUserRepository, BlacklistedUserRepository>()
            .AddScoped<IBookingRepository, BookingRepository>()
            .AddScoped<IBookingContractRepository, BookingContractRepository>()
            .AddScoped<IBookingDetailRepository, BookingDetailRepository>()
            .AddScoped<IContractRepository, ContractRepository>()
            .AddScoped<IDurationPriceRepository, DurationPriceRepository>()
            .AddScoped<IFeedbackRepository, FeedbackRepository>()
            .AddScoped<IHelperRepository, HelperRepository>()
            .AddScoped<IHelperAvailabilityRepository, HelperAvailabilityRepository>()
            .AddScoped<INotificationRepository, NotificationRepository>()
            .AddScoped<IRefundRepository, RefundRepository>()
            .AddScoped<IRoomPricingRepository, RoomPricingRepository>()
            .AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>()
            .AddScoped<IServiceTypeRepository, ServiceTypeRepository>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}