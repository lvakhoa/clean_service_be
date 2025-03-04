
using CleanService.Src.Modules.Booking.Mapping.Profiles;
using CleanService.Src.Modules.Booking.Services;

namespace CleanService.Src.Modules.Booking;

public static class BookingModule
{
    public static IServiceCollection AddBookingDependency(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();

        return services;
    }

    public static IServiceCollection AddBookingMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateBookingRequestProfile))
            .AddAutoMapper(typeof(BookingResponseProfile))
            .AddAutoMapper(typeof(UpdateBookingRequestProfile))
            .AddAutoMapper(typeof(CreateRefundProfile));

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
