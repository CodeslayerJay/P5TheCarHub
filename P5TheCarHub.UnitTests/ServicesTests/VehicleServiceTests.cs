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
        public void AddVehicle_WhenCalled_StoresAndReturnsNewVehicle()
        {
            
            var vehicle = new Vehicle
            {
                Year = 2001,
                Make = "Kia",
                Model = "Optima",
                Trim = "Ex",
                Mileage = 30000,
                VIN = "qwerty-1234",
                LotDate = DateTime.Today,
                PurchaseDate = DateTime.Today,
                PurchasePrice = 3000M,
                
            };

            var result = _vehicleService.AddVehicle(vehicle);
            
            Assert.NotNull(result);
            Assert.Equal("2001 Kia Optima Ex".ToUpper(), result.FullVehicleName);
            Assert.Equal(3500M, result.SalePrice);

        }

        [Fact]
        public void GetVehicle_ById_ReturnsVehicleOrNullWhenNotFound()
        {
            var result = _vehicleService.GetVehicle(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetVehicle_ByVin_ReturnsVehicleOrNullWhenNotFound()
        {
            var result = _vehicleService.GetVehicle("1234-1234");

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateVehicle_WhenFound_UpdatesAndReturnsVehicle()
        {
            var vehicle = _vehicleService.GetVehicle(2);
            var orgMakeValue = vehicle.Make;
            vehicle.Make = "New Make";

            var result = _vehicleService.UpdateVehicle(vehicle);

            Assert.NotNull(result);
            Assert.NotEqual(orgMakeValue, result.Make);

        }

        [Fact]
        public void UpdateVehicle_WhenNotFound_ReturnsNull()
        {
            var vehicle = new Vehicle();
            
            var result = _vehicleService.UpdateVehicle(vehicle);

            Assert.Null(result);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsListOfVehicles()
        {
            var result = _vehicleService.GetAll();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
        }

        [Fact]
        public void GetAll_ByFullNameFilter_ReturnsListOfVehicles()
        {
            var filterByModel = _vehicleService.GetAll("Kia");
            Assert.NotEmpty(filterByModel);

            var filterByMake = _vehicleService.GetAll("Optima");
            Assert.NotEmpty(filterByMake);

            var filterByYear = _vehicleService.GetAll("2008");
            Assert.NotEmpty(filterByYear);

            var filterByTrim = _vehicleService.GetAll("Ex");
            Assert.NotEmpty(filterByTrim);
        }

        [Fact]
        public void GetAllByMake_WhenCalled_ReturnsListOfVehicles()
        {
            var result = _vehicleService.GetAllByMake("Kia");

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAllByModel_WhenCalled_ReturnsListOfVehicles()
        {
            var result = _vehicleService.GetAllByModel("Optima");

            Assert.NotEmpty(result);
        }

        [Fact]
        public void DeleteVehicle_WhenFound_FindsAndRemovesVehicle()
        {
            var seedVehicle = new Vehicle
            {
                Year = 2001,
                Make = "Kia",
                Model = "Optima",
                Trim = "Ex",
                Mileage = 30000,
                VIN = "zzz-1234",
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
        public void DeleteVehicle_WhenNotFound_DoesNothing()
        {
            var orgVehicleCount = _vehicleService.GetAll().Count();
            _vehicleService.DeleteVehicle(99999);

            var newVehicleCount = _vehicleService.GetAll().Count();

            Assert.Equal(orgVehicleCount, newVehicleCount);
        }

        [Fact]
        public void GetVehiclesBySoldStatus_IsSoldTrue_ReturnsListOfVehiclesSold()
        {
            var result = _vehicleService.GetVehiclesBySoldStatus(true);

            Assert.NotEmpty(result);
            Assert.True(result.First().IsSold);
        }

        [Fact]
        public void GetVehiclesBySoldStatus_IsSoldFalse_ReturnsListOfVehiclesNotSold()
        {
            var result = _vehicleService.GetVehiclesBySoldStatus(false);

            Assert.NotEmpty(result);
            Assert.False(result.First().IsSold);
        }

        
    }
}
