
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.Profiles;
using CleanService.Src.Modules.Manage.Services;

namespace CleanService.Src.Modules.Manage;

public static class ManageModule
{
    public static IServiceCollection AddManageDependency(this IServiceCollection services)
    {
        services.AddScoped<IManageService, ManageService>();

        return services;
    }

    public static IServiceCollection AddManageMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(HelperDetailResponseProfile))
            .AddAutoMapper(typeof(RefundProfile))
            .AddAutoMapper(typeof(RoomPricingProfile))
            .AddAutoMapper(typeof(DurationPriceProfile))
            .AddAutoMapper(typeof(AdminUpdateUserRequestDto))
            .AddAutoMapper(typeof(AdminUpdateHelperRequestProfile));
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
