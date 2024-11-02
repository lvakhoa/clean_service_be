using CleanService.Src.Modules.ServicePricing.Repositories;

namespace CleanService.Src.Modules.ServicePricing;

public static class ServicePricingModule
{
    public static IServiceCollection AddServicePricingDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IServicePricingRepository, ServicePricingRepository>();

        return services;
    }
}