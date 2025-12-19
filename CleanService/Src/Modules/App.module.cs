using CleanService.Src.Modules.Auth;
using CleanService.Src.Modules.Booking;
using CleanService.Src.Modules.Contract;
using CleanService.Src.Modules.Mail;
using CleanService.Src.Modules.Manage;
using CleanService.Src.Modules.Payment;
using CleanService.Src.Modules.Scheduler;
using CleanService.Src.Modules.ServiceCategory;
using CleanService.Src.Modules.ServiceType;
using CleanService.Src.Modules.Storage;
using CleanService.Src.Utils.RequestClient;


using HttpClientHandler = CleanService.Src.Utils.HttpClientHandler;

namespace CleanService.Src.Modules;

public static class AppModule
{
    public static IServiceCollection AddAppDependency(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Inject Configuration
        services
            .AddSingleton<IConfiguration>(builder.Configuration);

        services
            .AddTransient<IRequestClient, RequestClient>();

        services
            .AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<HttpClientHandler>();

        // Inject Auth Module
        services
            .AddAuthModule(builder);

        // Inject Mail Module
        services
            .AddMailModule(builder.Configuration);

        // Inject Booking Module
        services
            .AddBookingModule();

        // Inject Service Type Module
        services
            .AddServiceTypeModule();

        // Inject Service Category Module
        services
            .AddServiceCategoryModule();

        // Inject Contract Module
        services
            .AddContractModule();

        // Inject Payment Module
        services
            .AddPaymentModule();

        // Inject Scheduler Module
        services
            .AddSchedulerModule();

        // Inject Manage Module
        services
            .AddManageModule();

        // Inject Cloudinary Module
        services
            .AddStorageModule();

        return services;
    }
}
