using P5TheCarHub.Core.Interfaces.Managers;

namespace P5TheCarHub.Core.Interfaces.Managers
{
    public interface IPhotoManager<T>
    {
        IPhotoManagerResult Result { get; }
        string FolderPath { get; }
        string UploadPath { get; }

        void DeleteImageFromDisk(string imageUrl);
        IPhotoManagerResult UploadImage(T image, int? identifier);
        bool ValidateImage(T formFile);
        void SetUploadPath(string uploadPath);
        void SetFolderPath(string folderPath);
    }
}