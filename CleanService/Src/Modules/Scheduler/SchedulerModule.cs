using CleanService.Src.Modules.Booking;
using CleanService.Src.Modules.Booking.Infrastructures;
using CleanService.Src.Modules.Booking.Mapping.Profiles;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Scheduler.Services;

namespace CleanService.Src.Modules.Scheduler;

public static class SchedulerModule
{
    public static IServiceCollection AddSchedulerDependency(this IServiceCollection services)
    {
        services
            .AddScoped<ISchedulerService, SchedulerService>()
            .AddScoped<IBookingUnitOfWork, BookingUnitOfWork>();

        return services;
    }
    
    public static IServiceCollection AddSchedulerMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(BookingResponseProfile))
            .AddAutoMapper(typeof(UpdateBookingRequestProfile));

        return services;
    }
    
    public static IServiceCollection AddSchedulerModule(this IServiceCollection services)
    {
        services
            .AddSchedulerDependency()
            .AddSchedulerMapping();

        return services;
    }
}