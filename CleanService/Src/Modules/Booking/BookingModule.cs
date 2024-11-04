
using CleanService.Src.Modules.Booking.Infrastructures;
using CleanService.Src.Modules.Booking.Mapping.Profiles;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceType;

namespace CleanService.Src.Modules.Booking;

public static class BookingModule
{
    public static IServiceCollection AddBookingDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IBookingService, BookingService>()
            .AddScoped<IBookingUnitOfWork, BookingUnitOfWork>();

        return services;
    }

    public static IServiceCollection AddBookingMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateBookingRequestProfile))
            .AddAutoMapper(typeof(BookingResponseProfile))
            .AddAutoMapper(typeof(UpdateBookingRequestProfile));

        return services;
    }
    
    public static IServiceCollection AddBookingModule(this IServiceCollection services)
    {
        services
            .AddBookingDependency()
            .AddBookingMapping();

        return services;
    }
}