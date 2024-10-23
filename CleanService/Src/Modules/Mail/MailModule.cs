using CleanService.Src.Modules.Mail.Services;
using CleanService.Src.Modules.Notification.Services;
using Resend;

namespace CleanService.Src.Modules.Mail;

public static class MailModule
{
    public static IServiceCollection AddMailModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions();
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>( o =>
        {
            o.ApiToken = config.GetValue<string>("Resend:ApiKey")!;
        } );
        services.AddTransient<IResend, ResendClient>();
        services.AddScoped<IMailService, ResendService>();
        return services;
    }
}