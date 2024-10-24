

using CleanService.Src.Modules.Service.Repositories;
using CleanService.Src.Modules.Service.Services;

namespace CleanService.Src.Modules.Service;

public static class ServiceModule
{
    public static IServiceCollection AddServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        
        return services;
    }
}

