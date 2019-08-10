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
        private readonly PhotoService _photoService;
        private readonly PhotoRepositoryMock _photoRepo;

        public PhotoServiceTests()
        {
            _photoRepo = new PhotoRepositoryMock();
            var vehicleRepo = new VehicleRepositoryMock();
            _photoService = new PhotoService(_photoRepo, vehicleRepo);
        }

        [Fact]
        public void AddPhoto_WhenVehicleIsFoundAndIsMain_ReturnsPhoto()
        {
            var photo = new Photo { VehicleId = 1, ImageUrl = "TEST" };

            var result = _photoService.AddPhoto(photo, isMain: true);

            Assert.NotNull(_photoService);
        }

        [Fact]
        public void AddPhoto_WhenVehicleNotFound_ThrowsVehicleNotFoundException()
        {
            var photo = new Photo { ImageUrl = "TEST" };

            Assert.Throws<VehicleNotFoundException>(() => _photoService.AddPhoto(photo, isMain: true));
        }

        [Fact]
        public void AddPhoto_WhenVehicleAlreadyHasMainPhoto_AddingNewMainPhotoUpdates()
        {
            var orignalPhoto = _photoRepo.GetAllByVehicleId(1).SingleOrDefault(x => x.IsMain == true);
            var photo = new Photo { VehicleId = 1, ImageUrl = "TEST", IsMain = true };

            var result = _photoService.AddPhoto(photo);
            var mainPhoto = _photoRepo.GetAllByVehicleId(1).SingleOrDefault(x => x.IsMain == true);

            Assert.NotNull(result);
            Assert.NotEqual(orignalPhoto, mainPhoto);
        }

        [Fact]
        public void AddPhoto_WhenVehicleDoesNotHaveMainPhoto_AddingNewPhotoIsSetToMainPhoto()
        {
            var photo = new Photo { VehicleId = 2, ImageUrl = "TEST" };

            var result = _photoService.AddPhoto(photo);
            
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

            var result = _photoService.AddPhoto(photo);
            var photosCount = _photoRepo.GetAllByVehicleId(vehicleId).Count();

            _photoService.DeletePhoto(result.Id);

            Assert.NotEqual(photosCount, _photoRepo.GetAllByVehicleId(vehicleId).Count());
        }

        [Fact]
        public void GetPhoto_WhenFound_ReturnsPhoto()
        {
            var result = _photoService.GetPhoto(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetPhoto_WhenNotFound_ThrowsPhotoNotFoundException()
        {
            Assert.Throws<PhotoNotFoundException>(() => _photoService.GetPhoto(999));
        }

        [Fact]
        public void UpdateVehicleMainPhoto_WhenFound_UpdatesMainPhoto()
        {
            var vehicleId = 1;
            var photoId = 2;
            var currentMainPhoto = _photoRepo.GetVehicleMainPhoto(vehicleId);

            _photoService.UpdateVehicleMainPhoto(vehicleId, photoId);
            var newMainPhoto = _photoRepo.GetVehicleMainPhoto(vehicleId);

            Assert.NotEqual(currentMainPhoto, newMainPhoto);
            Assert.Equal(newMainPhoto.Id, photoId);
        }
    }
}
