
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Contract.Services;
using CleanService.Src.Modules.Service.Services;

namespace CleanService.Src.Modules.Booking;

public static class BookingModule
{
    public static IServiceCollection AddBookingDependency(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IContractService, ContractService>();

        return services;
    }
}