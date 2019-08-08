using P5TheCarHub.Domain.Entities;
using P5TheCarHub.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class VehicleServiceTests
    {
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _vehicleService = new VehicleService();

        }

        [Fact]
        public void AddVehicle_WhenCalled_AddsFullVehicleNameAndReturnsNewlyCreatedVehicle()
        {
            
            var vehicle = new Vehicle
            {
                Year = 2001,
                Make = "Kia",
                Model = "Optima",
                Trim = "Ex",
                Mileage = 30000,
                VIN = "1234-1234",
                LotDate = DateTime.Today,
                PurchaseDate = DateTime.Today,
                PurchasePrice = 3000M,
                SalePrice = 500
            };

            var result = _vehicleService.AddVehicle(vehicle);
            
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal("2001 Kia Optima Ex", result.FullVehicleName);
        }

        [Fact]
        public void GetVehicle_WhenCalled_ReturnsVehicleById()
        {
            var result = _vehicleService.GetVehicle(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateVehicle_WhenVehicleIdNotZero_FindsUpdatesAndReturnsVehicle()
        {
            var vehicle = _vehicleService.GetVehicle(2);
            var orgMakeValue = vehicle.Make;
            vehicle.Make = "New Make";

            var result = _vehicleService.UpdateVehicle(vehicle);

            Assert.NotNull(result);
            Assert.NotEqual(orgMakeValue, result.Make);

        }

        [Fact]
        public void UpdateVehicle_WhenVehicleIdIsZero_ReturnsNull()
        {
            var vehicle = new Vehicle();
            
            var result = _vehicleService.UpdateVehicle(vehicle);

            Assert.Null(result);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsIEnumberableOfVehicles()
        {
            var result = _vehicleService.GetAll();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
        }

        [Fact]
        public void DeleteVehicle_WhenNotNull_FindsAndRemovesVehicle()
        {
            var seedVehicle = new Vehicle
            {
                Year = 2001,
                Make = "Kia",
                Model = "Optima",
                Trim = "Ex",
                Mileage = 30000,
                VIN = "1234-1234",
                LotDate = DateTime.Today,
                PurchaseDate = DateTime.Today,
                PurchasePrice = 3000M,
                SalePrice = 500
            };

            var vehicle = _vehicleService.AddVehicle(seedVehicle);
            Assert.NotNull(vehicle);

            _vehicleService.DeleteVehicle(vehicle.Id);

            Assert.Null(_vehicleService.GetVehicle(vehicle.Id));
        }

        [Fact]
        public void DeleteVehicle_WhenNull_DoesNothing()
        {
            var orgVehicleCount = _vehicleService.GetAll().Count();
            _vehicleService.DeleteVehicle(99999);

            var newVehicleCount = _vehicleService.GetAll().Count();

            Assert.Equal(orgVehicleCount, newVehicleCount);
        }
    }
}
