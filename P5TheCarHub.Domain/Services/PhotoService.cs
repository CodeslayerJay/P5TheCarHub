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

        public Photo SavePhoto(Photo photo, bool isMain = false)
        {
            var vehicle = _unitOfWork.Vehicles.GetById(photo.VehicleId);

            var spec = new VehicleExistsSpecification();
            if(!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(photo.VehicleId);
            

            if(photo.IsMain || isMain)
            {
                var mainPhoto = _unitOfWork.Photos.GetVehicleMainPhoto(photo.VehicleId);

                if (mainPhoto != null)
                    mainPhoto.IsMain = false;
            }
                
            
            if (!CheckCurrentMainPhotoExists(photo.VehicleId))
                photo.IsMain = true;

            if(photo.Id == 0)
                _unitOfWork.Photos.Add(photo);

            _unitOfWork.SaveChanges();

            return photo;
            
        }

        private bool CheckCurrentMainPhotoExists(int vehicleId)
        {
            var photo = GetVehicleMainPhoto(vehicleId);

            return (photo != null) ? true : false;
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
        
            if (photo.IsMain)
            {
                var newMainPhoto = _unitOfWork.Photos.GetFirstPhotoNotSetAsMain(photo.VehicleId);

                if (newMainPhoto != null)
                    newMainPhoto.IsMain = true;
            }

            _unitOfWork.Photos.Delete(id);
            _unitOfWork.SaveChanges();

        }

        public Photo GetPhoto(int id)
        {
            return _unitOfWork.Photos.GetById(id);
        }

        public void UpdateVehicleMainPhoto(int oldMainPhotoId, int newMainPhotoId)
        {
            var currentMainPhoto = _unitOfWork.Photos.GetById(oldMainPhotoId);
            
            if (currentMainPhoto == null)
                throw new PhotoNotFoundException(oldMainPhotoId);

            currentMainPhoto.IsMain = false;

            var newMainPhoto = _unitOfWork.Photos.GetById(newMainPhotoId);
            if (newMainPhoto == null)
                throw new PhotoNotFoundException(newMainPhotoId);

            newMainPhoto.IsMain = true;
            _unitOfWork.SaveChanges();
        }
    }
}
