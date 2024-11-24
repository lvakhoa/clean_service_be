using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CleanService.Src.Modules.Storage.Services;

public class StorageService: IStorageService
{
    private readonly Cloudinary _cloudinary;
    
    public StorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    
    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
    {
        long size = file.Length;
        
        var image = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream())
        };
        
        return await _cloudinary.UploadAsync(image);
    }

    public async Task<RawUploadResult> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }
        
        using var stream = file.OpenReadStream();
        
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = $"files/{Path.GetFileNameWithoutExtension(file.FileName)}",
        };
        
        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception("Failed to upload file.");
        }

        return result;
    }
}