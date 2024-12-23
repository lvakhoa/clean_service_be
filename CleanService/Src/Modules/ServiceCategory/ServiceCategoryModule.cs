using CleanService.Src.Modules.ServiceCategory.Infrastructures;
using CleanService.Src.Modules.ServiceCategory.Mapping.Profiles;
using CleanService.Src.Modules.ServiceCategory.Services;

namespace CleanService.Src.Modules.ServiceCategory;

public static class ServiceCategoryModule
{
    public static IServiceCollection AddServiceCategoryDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IServiceCategoryService, ServiceCategoryService>()
            .AddScoped<IServiceCategoryUnitOfWork, ServiceCategoryUnitOfWork>();
        
        return services;
    }
    
    public static IServiceCollection AddServiceCategoryMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateServiceCategoryRequestProfile))
            .AddAutoMapper(typeof(ServiceCategoryResponseProfile))
            .AddAutoMapper(typeof(UpdateServiceCategoryRequestProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServiceCategoryModule(this IServiceCollection services)
    {
        services
            .AddServiceCategoryDependency()
            .AddServiceCategoryMapping();

        return services;
    }
}