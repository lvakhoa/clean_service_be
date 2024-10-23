
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Booking.Services;

namespace CleanService.Src.Modules.Booking;

public static class BookingModule
{
    public static IServiceCollection AddBookingDependency(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}