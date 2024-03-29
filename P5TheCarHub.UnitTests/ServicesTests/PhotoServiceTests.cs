﻿using Moq;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Services;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class PhotoServiceTests
    {
        private readonly UnitOfWorkMock _unitOfWork;
        private readonly PhotoService _photoService;
        
        public PhotoServiceTests()
        {
            _unitOfWork = new UnitOfWorkMock();
            _photoService = new PhotoService(_unitOfWork);
        }

        [Fact]
        public void SavePhoto_WhenVehicleIsFoundAndIsMain_ReturnsPhoto()
        {
            var photo = new Photo { VehicleId = 1, ImageUrl = "TEST" };

            var result = _photoService.SavePhoto(photo, isMain: true);

            Assert.NotNull(_photoService);
        }

        [Fact]
        public void SavePhoto_WhenVehicleNotFound_ThrowsVehicleNotFoundException()
        {
            var photo = new Photo { ImageUrl = "TEST" };

            Assert.Throws<VehicleNotFoundException>(() => _photoService.SavePhoto(photo, isMain: true));
        }

        [Fact]
        public void SavePhoto_WhenVehicleAlreadyHasMainPhoto_AddingNewMainPhotoUpdates()
        {
            var orignalPhoto = _unitOfWork.Photos.GetAllByVehicleId(1).SingleOrDefault(x => x.IsMain == true);
            var photo = new Photo { VehicleId = 1, ImageUrl = "TEST", IsMain = true };

            var result = _photoService.SavePhoto(photo);
            var mainPhoto = _unitOfWork.Photos.GetAllByVehicleId(1).SingleOrDefault(x => x.IsMain == true);

            Assert.NotNull(result);
            Assert.NotEqual(orignalPhoto, mainPhoto);
        }

        [Fact]
        public void SavePhoto_WhenVehicleDoesNotHaveMainPhoto_AddingNewPhotoIsSetToMainPhoto()
        {
            var photo = new Photo { VehicleId = 2, ImageUrl = "TEST" };

            var result = _photoService.SavePhoto(photo);
            
            Assert.NotNull(result);
            Assert.True(result.IsMain);
        }

        [Fact]
        public void GetVehicleMainPhoto_WhenCalled_ReturnsPhoto()
        {
            var vehicleId = 1;
            var result = _photoService.GetVehicleMainPhoto(vehicleId);

            Assert.NotNull(result);
            Assert.Equal(vehicleId, result.VehicleId);
        }

        [Fact]
        public void DeletePhoto_WhenFound_DeletesPhoto()
        {
           var vehicleId = 1;
           var photo = new Photo { VehicleId = vehicleId, ImageUrl = "TEST", IsMain = true };

            var result = _photoService.SavePhoto(photo);
            var photosCount = _unitOfWork.Photos.GetAllByVehicleId(vehicleId).Count();

            _photoService.DeletePhoto(result.Id);

            Assert.NotEqual(photosCount, _unitOfWork.Photos.GetAllByVehicleId(vehicleId).Count());
        }

        [Fact]
        public void GetPhoto_WhenFound_ReturnsPhoto()
        {
            var result = _photoService.GetPhoto(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateVehicleMainPhoto_WhenFound_UpdatesMainPhoto()
        {
            var vehicleId = 1;
            var photoId = 2;
            var currentMainPhoto = _unitOfWork.Photos.GetVehicleMainPhoto(vehicleId);

            _photoService.UpdateVehicleMainPhoto(currentMainPhoto.Id, photoId);
            var newMainPhoto = _unitOfWork.Photos.GetVehicleMainPhoto(vehicleId);

            Assert.NotEqual(currentMainPhoto, newMainPhoto);
            Assert.Equal(newMainPhoto.Id, photoId);
        }
    }
}
