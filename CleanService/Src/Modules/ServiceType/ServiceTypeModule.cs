using CleanService.Src.Modules.Service.Services;
using CleanService.Src.Modules.ServiceType.Mapping.Profiles;
using CleanService.Src.Modules.ServiceType.Repositories;
using CleanService.Src.Modules.ServiceType.Services;

namespace CleanService.Src.Modules.Service;

public static class ServiceTypeModule
{
    public static IServiceCollection AddServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddServiceMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateServiceRequestProfile))
            .AddAutoMapper(typeof(ServiceTypeResponseProfile));
        
        return services;
    }
    
    public static IServiceCollection AddServiceModule(this IServiceCollection services)
    {
        services.AddServiceDependency();

        return services;
    }
}

