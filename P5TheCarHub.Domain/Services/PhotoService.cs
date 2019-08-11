using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Photo AddPhoto(Photo photo, bool isMain = false)
        {
            var vehicle = _unitOfWork.Vehicles.GetById(photo.VehicleId);

            var spec = new VehicleExistsSpecification();
            if(!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(photo.VehicleId);
            

            if(photo.IsMain || isMain)
                SetNewMainPhoto(photo.VehicleId);

            
            if (!CheckCurrentMainPhotoExists(photo.VehicleId))
                photo.IsMain = true;

            var newPhoto = _unitOfWork.Photos.Add(photo);
            _unitOfWork.SaveChanges();

            return newPhoto;
            
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
            return _unitOfWork.Photos.GetVehicleMainPhoto(vehicleId);
        }

        public void DeletePhoto(int id)
        {
            var photo = GetPhoto(id);

            if (photo == null)
                throw new PhotoNotFoundException(id);

            _unitOfWork.Photos.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public Photo GetPhoto(int id)
        {
            var photo = _unitOfWork.Photos.GetById(id);

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
            _unitOfWork.SaveChanges();
        }
    }
}
