
using CleanService.Src.Modules.Manage.Infrastructures;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.Profiles;
using CleanService.Src.Modules.Manage.Services;
using CleanService.Src.Repositories.Contract;
using CleanService.Src.Repositories.DurationPrices;
using CleanService.Src.Repositories.RoomPricings;
using CleanService.Src.Repositories.ServiceType;

namespace CleanService.Src.Modules.Manage;

public static class ManageModule
{
    public static IServiceCollection AddManageDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IManageService, ManageService>()
            .AddScoped<IManageUnitOfWork, ManageUnitOfWork>();

        return services;
    }

    public static IServiceCollection AddManageMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(HelperDetailResponseProfile))
            .AddAutoMapper(typeof(RefundProfile))
            .AddAutoMapper(typeof(RoomPricingProfile))
            .AddAutoMapper(typeof(DurationPriceProfile));
        return services;
    }
    
    public static IServiceCollection AddManageModule(this IServiceCollection services)
    {
        services
            .AddManageDependency()
            .AddManageMapping();

        return services;
    }
}