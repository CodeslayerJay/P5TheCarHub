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
    public class PhotoManager : IPhotoManager<IFormFile>
    {
        
        public IPhotoManagerResult Result { get; }

        private readonly IHostingEnvironment _host;
        
        public string FolderPath { get; private set; } = @"img_uploads\";
        public string UploadPath { get; private set; }

        private string _imageFileName;

        public PhotoManager(IHostingEnvironment hostingEnvironment)
        {
            Result = new PhotoManagerResult();
            _host = hostingEnvironment;
        }

        public PhotoManager(IHostingEnvironment hostingEnvironment, string folderPath, string uploadPath)
        {
            Result = new PhotoManagerResult();
            _host = hostingEnvironment;
            UploadPath = uploadPath;
            FolderPath = folderPath;
        }

        public void SetUploadPath(string uploadPath)
        {
            if (!String.IsNullOrEmpty(uploadPath))
                UploadPath = uploadPath;
        }

        public void SetFolderPath(string folderPath)
        {
            if (!String.IsNullOrEmpty(folderPath))
                FolderPath = folderPath;
        }
                
        public bool ValidateImage(IFormFile formFile)
        {

            if (formFile != null && formFile.Length > 0)
            {

                if (formFile.ContentType.Contains("image"))
                {
                    Result.IsValidImage = true;
                }
                    
            }

            return Result.IsValidImage;
        }

        public IPhotoManagerResult UploadImage(IFormFile image, int? identifier)
        {
            if (ValidateImage(image))
            {
                GenerateImagePath(image, identifier);
                StoreImageToDisk(image);
                SetImagePathAndUrl();
            }

            Result.Success = true;
            return Result;
        }

        private void GenerateImagePath(IFormFile image, int? identifier)
        {
            if (identifier.HasValue)
                FolderPath = FolderPath + $"ID_{identifier.Value}\\";

            UploadPath = Path.Combine(_host.WebRootPath, FolderPath);

            _imageFileName = Guid.NewGuid().ToString().Replace("-", "") +
                    Path.GetExtension(image.FileName);

        }

        private void StoreImageToDisk(IFormFile image)
        {
            if (String.IsNullOrEmpty(UploadPath) || String.IsNullOrEmpty(_imageFileName))
                GenerateImagePath(image, identifier: null);

            try
            {
                Directory.CreateDirectory(UploadPath);

                using (var fileStream = new FileStream(Path.Combine(UploadPath, _imageFileName), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                
            }
            catch (Exception ex)
            {
                Result.Error = ex;
            }
        }

        private void SetImagePathAndUrl()
        {
            Result.ImageFullPath = $"\\{UploadPath}{_imageFileName}";
            Result.ImageUrl = $"\\{FolderPath}{_imageFileName}";
        }

        public IPhotoManagerResult DeleteImageFromDisk(string imageUrl)
        {
            var webRoot = _host.WebRootPath;
            var filePath = webRoot + imageUrl;
            try
            {
                File.Delete(filePath);
                Result.Success = true;

            }
            catch(Exception ex)
            {
                Result.Error = ex;
                
            }

            return Result;
        }
    }
}
