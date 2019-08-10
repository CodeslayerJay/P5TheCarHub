using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class PhotoService
    {
        private readonly IPhotoRepository _photoRepo;
        private readonly IVehicleRepository _vehicleRepo;

        public PhotoService(IPhotoRepository photoRepository, IVehicleRepository vehicleRepository)
        {
            _photoRepo = photoRepository;
            _vehicleRepo = vehicleRepository;
        }

        public Photo AddPhoto(Photo photo, bool isMain = false)
        {
            var vehicle = _vehicleRepo.GetById(photo.VehicleId);

            var spec = new VehicleExistsSpecification(_vehicleRepo);
            if(!spec.IsSatisfiedBy(photo.VehicleId))
                throw new VehicleNotFoundException(photo.VehicleId);
            

            if(photo.IsMain || isMain)
                SetNewMainPhoto(photo.VehicleId);

            
            if (!CheckCurrentMainPhotoExists(photo.VehicleId))
                photo.IsMain = true;

            return _photoRepo.Add(photo);
            
        }

        private bool CheckCurrentMainPhotoExists(int vehicleId)
        {
            var photo = GetVehicleMainPhoto(vehicleId);

            return (photo != null) ? true : false;
        }

        private void SetNewMainPhoto(int vehicleId)
        {
            var currentMainPhoto = GetVehicleMainPhoto(vehicleId);

            if (currentMainPhoto != null)
                currentMainPhoto.IsMain = false;
        }

        public Photo GetVehicleMainPhoto(int vehicleId)
        {
            return _photoRepo.GetVehicleMainPhoto(vehicleId);
        }

        public void DeletePhoto(int id)
        {
            var photo = GetPhoto(id);

            if (photo == null)
                throw new PhotoNotFoundException(id);

            _photoRepo.Delete(id);
        }

        public Photo GetPhoto(int id)
        {
            var photo = _photoRepo.GetById(id);

            if (photo == null)
                throw new PhotoNotFoundException(id);

            return photo;
        }

        public void UpdateVehicleMainPhoto(int vehicleId, int newMainPhotoId)
        {
            var currentMainPhoto = GetVehicleMainPhoto(vehicleId);

            var photo = GetPhoto(newMainPhotoId);

            if (photo == null)
                throw new PhotoNotFoundException(newMainPhotoId);

            if(currentMainPhoto != null)
            {
                SetNewMainPhoto(currentMainPhoto.VehicleId);
            }

            photo.IsMain = true;
        }
    }
}
