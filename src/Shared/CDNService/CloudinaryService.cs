using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class CloudinaryService: ICloudinaryService
    {
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;
        private readonly Account _account;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinarySettings = cloudinarySettings.Value;
            _cloudinary = new Cloudinary(_account = SetupCloudinaryAccount());
        }


        public async Task<string> UploadDocument(string fileName, IFormFile filePath) // Change to a List of IForm  File.  // Change the file Name to Upload Service
        {
            string fileUrl = default;

            if (filePath is null)
            {
               // Add Logging
                return ("Failed");
            }

            try
            {
                _cloudinary.Api.Secure = true;

                var stream = filePath.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, stream)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                fileUrl = uploadResult.SecureUrl.AbsoluteUri;
                
                return fileUrl;
            }
            catch (Exception ex)
            {
                // Add logging 
                return $"{ex.Message}, An Error Occcured while uploading file to cloudinary";
            }
        }


        private Account SetupCloudinaryAccount()
        {
            return new Account()
            {
                Cloud = _cloudinarySettings.CloudName,
                ApiKey = _cloudinarySettings.ApiKey,
                ApiSecret = _cloudinarySettings.ApiSecret
            };

        }
    }
}
