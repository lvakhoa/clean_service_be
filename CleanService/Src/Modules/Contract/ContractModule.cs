using CleanService.Src.Modules.Contract.Repositories;
using CleanService.Src.Modules.Contract.Services;

namespace CleanService.Src.Modules.Contract;

public static class ContractModule
{
    public static IServiceCollection AddContractDependency(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IContractRepository, ContractRepository>();

        return services;
    }
}