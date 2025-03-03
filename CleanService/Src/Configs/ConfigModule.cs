using CleanService.Src.Modules;

using FluentValidation;
using FluentValidation.AspNetCore;

namespace CleanService.Src.Configs;

public static class ConfigModule
{
    public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
    {
        // Inject Swagger
        services.AddSwagger();

        // Inject Data Access
        services.AddDataAccess(configuration);

        // Inject Fluent Validation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(IModuleMarker));

        // Inject AutoMapper
        services.AddAutoMapper(typeof(IModuleMarker));
    }
}
