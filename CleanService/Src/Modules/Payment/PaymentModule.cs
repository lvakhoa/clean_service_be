using CleanService.Src.Modules.Payment.Infrastructures;
using CleanService.Src.Modules.Payment.Services;

namespace CleanService.Src.Modules.Payment;

public static class PaymentModule
{
    public static IServiceCollection AddPaymentDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IPaymentService, PayOsService>()
            .AddScoped<IPaymentUnitOfWork, PaymentUnitOfWork>();
        return services;
    }
    
    public static IServiceCollection AddPaymentModule(this IServiceCollection services)
    {
        services
            .AddPaymentDependency();
        return services;
    }
}