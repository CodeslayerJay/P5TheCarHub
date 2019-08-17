using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using P5TheCarHub.Core.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.Managers
{
    public class PhotoManager
    {
        
        public IPhotoManagerResult Result { get; }
        private const string IMAGE_FOLDER_PATH = @"img_uploads\";

        private readonly IHostingEnvironment _host;
        private readonly PhotoManagerResult _photoResult;

        private string _uploadPath;
        private string _imageFileName;

        public PhotoManager(IHostingEnvironment hostingEnvironment)
        {
            Result = new PhotoManagerResult();
            _host = hostingEnvironment;
        }
                
        public IPhotoManagerResult ValidateImage(IFormFile formFile)
        {

            if (formFile != null && formFile.Length > 0)
            {

                if (formFile.ContentType.Contains("image"))
                {
                    Result.IsValidImage = true;
                    return Result;
                }
                    
            }

            return Result;
        }

        public IPhotoManagerResult UploadImage(IFormFile image, int? identifier)
        {

            var result = ValidateImage(image);
            if (result.IsValidImage)
            {
                GenerateImagePath(image, identifier);

                StoreImageToDisk(image);
            }

            return Result;
        }

        private void GenerateImagePath(IFormFile image, int? identifier)
        {
            var folderPath = IMAGE_FOLDER_PATH;
            _uploadPath = Path.Combine(_host.WebRootPath, folderPath);

            if (identifier.HasValue)
                _uploadPath = Path.Combine(_uploadPath, $"_ID_{identifier.Value}");

            _imageFileName = Guid.NewGuid().ToString().Replace("-", "") +
                    Path.GetExtension(image.FileName);

        }

        private void StoreImageToDisk(IFormFile image)
        {
            if (String.IsNullOrEmpty(_uploadPath) || String.IsNullOrEmpty(_imageFileName))
                GenerateImagePath(image, identifier: null);

            try
            {
                using (var fileStream = new FileStream(Path.Combine(_uploadPath, _imageFileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                SetImagePath();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happened while attempting to upload image to disk. { ex.Message }");
            }
        }

        private void SetImagePath()
        {
            Result.ImagePath = "\\" + _uploadPath + "\\" + _imageFileName;
        }

        public void DeleteImageFromDisk(string imageUrl)
        {
            var webRoot = _host.WebRootPath;
            var filePath = webRoot + imageUrl;
            try
            {
                File.Delete(filePath);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error occurred attempting to remove image from disk. {ex.Message}");
            }
            
        }
    }
}
