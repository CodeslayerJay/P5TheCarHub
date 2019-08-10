using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
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

            if (vehicle == null)
                throw new VehicleNotFoundException(photo.VehicleId);

            if(photo.IsMain || isMain)
                SetNewMainPhoto(photo.VehicleId);
            
            //TODO: Determine if vehicle has no current main photo and set as main
            return _photoRepo.Add(photo);
            
        }

        private void SetNewMainPhoto(int vehicleId)
        {
            var currentMainPhoto = _photoRepo.GetVehicleMainPhoto(vehicleId);

            if (currentMainPhoto != null)
                currentMainPhoto.IsMain = false;
        }
    }
}
