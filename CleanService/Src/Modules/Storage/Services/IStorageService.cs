using CloudinaryDotNet.Actions;

namespace CleanService.Src.Modules.Storage.Services;

public interface IStorageService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    
    Task<RawUploadResult> UploadFileAsync(IFormFile file);
}