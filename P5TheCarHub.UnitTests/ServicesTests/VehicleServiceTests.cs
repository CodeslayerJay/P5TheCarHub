﻿using P5TheCarHub.Core.Entities;
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
    public class VehicleServiceTests
    {
        private readonly UnitOfWorkMock _unitOfWork;
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _unitOfWork = new UnitOfWorkMock();
            _vehicleService = new VehicleService(_unitOfWork);
        }

        [Fact]
        public void GetFullVehicleName_WhenCalled_ReturnsFullVehicleNameAsString()
        {
            var vehicle = _vehicleService.GetVehicle(id: 1);
            var result = _vehicleService.GetFullVehicleName(vehicle);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Contains(vehicle.Make.ToUpper(), result);
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
                Mileage = "3,000",
                VIN = "qwerty-1234",
                LotDate = DateTime.Today,
                PurchaseDate = DateTime.Today,
                PurchasePrice = 3000M,
                
            };

            var result = _vehicleService.AddVehicle(vehicle);
            
            Assert.NotNull(result);
            Assert.Equal(3500M, result.SalePrice);

        }

        [Fact]
        public void AddVehicle_WhenVehicleYearIsNotGreaterThanRequiredYear_ThrowsVehicleNotGreaterThanRequiredYearException()
        {

            var vehicle = new Vehicle
            {
                Year = 1980,
                Make = "Kia",
                Model = "Optima",
                Trim = "Ex",
                Mileage = "3,000",
                VIN = "qwerty-1234",
                LotDate = DateTime.Today,
                PurchaseDate = DateTime.Today,
                PurchasePrice = 3000M,

            };

            Assert.Throws<VehicleNotGreaterThanRequiredYearException>(() => _vehicleService.AddVehicle(vehicle));

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
        public void UpdateVehicle_WhenNotFound_ThrowsVehicleNotFoundException()
        {
            var vehicle = new Vehicle();
            
            Assert.Throws<VehicleNotFoundException>(() => _vehicleService.UpdateVehicle(vehicle));
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsListOfVehicles()
        {
            var result = _vehicleService.GetAll();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
        }

        [Fact]
        public void GetAll_WithAmount_ReturnsListOfVehiclesBasedOnAmount()
        {
            var result = _vehicleService.GetAll(1);

            Assert.NotEmpty(result);
            Assert.Single(result);
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
                Mileage = "3,000",
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
        public void DeleteVehicle_WhenNotFound_ThrowsVehicleNotFoundException()
        {
            var orgVehicleCount = _vehicleService.GetAll().Count();
            
            Assert.Throws<VehicleNotFoundException>(() => _vehicleService.DeleteVehicle(99999));
        }

        [Fact]
        public void GetVehiclesBySoldStatus_IsSoldTrue_ReturnsListOfVehiclesSold()
        {
            var result = _vehicleService.GetVehiclesBySoldStatus(isSold: true);

            Assert.NotEmpty(result);
            Assert.True(result.First().IsSold);
        }

        [Fact]
        public void GetVehiclesBySoldStatus_IsSoldFalse_ReturnsListOfVehiclesNotSold()
        {
            var result = _vehicleService.GetVehiclesBySoldStatus(isSold: false);

            Assert.NotEmpty(result);
            Assert.False(result.First().IsSold);
        }

        
    }
}
