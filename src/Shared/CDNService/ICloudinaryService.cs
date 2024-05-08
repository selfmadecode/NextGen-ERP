using Microsoft.AspNetCore.Http;

namespace Shared.Entities
{
    public interface ICloudinaryService
    {
        Task<string> UploadDocument(string fileName, IFormFile filePath);
    }
}