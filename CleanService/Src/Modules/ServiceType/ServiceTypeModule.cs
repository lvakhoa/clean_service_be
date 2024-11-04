using CleanService.Src.Modules.ServiceType.Infrastructures;
using CleanService.Src.Modules.ServiceType.Mapping.Profiles;
using CleanService.Src.Modules.ServiceType.Services;

namespace CleanService.Src.Modules.ServiceType;

public static class ServiceTypeModule
{
    public static IServiceCollection AddServiceTypeDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IServiceTypeService, ServiceTypeService>()
        .AddScoped<IServiceTypeUnitOfWork, ServiceTypeUnitOfWork>();
        
        return services;
    }
    
    public static IServiceCollection AddServiceTypeMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateServiceTypeRequestProfile))
            .AddAutoMapper(typeof(ServiceTypeResponseProfile))
            .AddAutoMapper(typeof(UpdateServiceTypeRequestProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServiceTypeModule(this IServiceCollection services)
    {
        services
            .AddServiceTypeDependency()
            .AddServiceTypeMapping();

        return services;
    }
}

