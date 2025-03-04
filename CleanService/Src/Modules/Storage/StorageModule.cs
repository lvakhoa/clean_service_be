using CleanService.Src.Modules.Storage.Services;


namespace CleanService.Src.Modules.Storage;

public static class StorageModule
{
    private static IServiceCollection AddCloudinaryDependency(this IServiceCollection services)
    {
        var cloudinaryUrl = services.BuildServiceProvider().GetService<IConfiguration>()?["CLOUDINARY_URL"];
        
        if (string.IsNullOrEmpty(cloudinaryUrl))
        {
            throw new InvalidOperationException("Cloudinary configuration is missing.");
        }
        
        var cloudinary = new CloudinaryDotNet.Cloudinary(cloudinaryUrl);
        
        services
            .AddSingleton(cloudinary);
        
        return services;
    }

    private static IServiceCollection AddStorageDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IStorageService, StorageService>();
        
        return services;
    }

    public static IServiceCollection AddStorageModule(this IServiceCollection services)
    {
        services
            .AddCloudinaryDependency()
            .AddStorageDependency();
        
        return services;
    }
}