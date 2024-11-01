
using CleanService.Src.Modules.Booking.Mapping.Profiles;
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Modules.Contract.Services;
using CleanService.Src.Modules.Service.Services;
using CleanService.Src.Modules.ServiceType.Services;

namespace CleanService.Src.Modules.Booking;

public static class BookingModule
{
    public static IServiceCollection AddBookingDependency(this IServiceCollection services)
    {
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
        services.AddScoped<IContractService, ContractService>();

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