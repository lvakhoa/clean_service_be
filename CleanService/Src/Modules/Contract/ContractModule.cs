using CleanService.Src.Modules.Contract.Mapping.Profiles;
using CleanService.Src.Modules.Contract.Services;

namespace CleanService.Src.Modules.Contract;

public static class ContractModule
{
    public static IServiceCollection AddContractMapping(this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(CreateContractRequestProfile))
            .AddAutoMapper(typeof(UpdateContractRequestProfile));

        return services;
    }

    public static IServiceCollection AddContractDependency(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();

        return services;
    }

    public static IServiceCollection AddContractModule(this IServiceCollection services)
    {
        services
            .AddContractDependency()
            .AddContractMapping();

        return services;
    }
}
