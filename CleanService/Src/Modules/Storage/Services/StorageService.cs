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
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            PublicId = $"files/{Path.GetFileNameWithoutExtension(file.FileName)}",
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
    
    public async Task DeleteFileAsync(string url)
    {
        try
        {
            var uri = new Uri(url);
            var pathSegments = uri.AbsolutePath.Split('/');
        
            var decodedSegments = pathSegments.Select(Uri.UnescapeDataString).ToArray();
            
            var fileName = decodedSegments.Last();
        
            var publicId = 
                "files/" + Path.GetFileNameWithoutExtension(fileName);
            
            if (string.IsNullOrEmpty(publicId))
            {
                throw new ArgumentException("PublicId cannot be null or empty.");
            }

            var deletionParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.StatusCode != System.Net.HttpStatusCode.OK || result.Result != "ok")
            {
                throw new Exception($"Failed to delete file with PublicId: {publicId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("File not found.");
        }
    }
}