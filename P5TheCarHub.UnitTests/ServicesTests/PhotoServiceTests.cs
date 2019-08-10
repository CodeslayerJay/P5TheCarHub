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
    }
}
