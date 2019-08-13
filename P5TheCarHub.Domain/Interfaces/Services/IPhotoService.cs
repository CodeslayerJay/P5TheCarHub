using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IPhotoService
    {
        Photo SavePhoto(Photo photo, bool isMain = false);
        void DeletePhoto(int id);
        Photo GetPhoto(int id);
        Photo GetVehicleMainPhoto(int vehicleId);
        void UpdateVehicleMainPhoto(int vehicleId, int newMainPhotoId);
    }
}