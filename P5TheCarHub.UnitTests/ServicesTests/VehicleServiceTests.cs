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
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _vehicleService = new VehicleService(new MockVehicleRepository());
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
        public void UpdateVehicle_WhenNotFound_ThrowsVehicleNotFoundException()
        {
            var vehicle = new Vehicle();
            
            //var result = _vehicleService.UpdateVehicle(vehicle);
            
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
        public void GetAll_FilterByModel_ReturnsListOfVehicles()
        {
            var results = _vehicleService.GetAll("Kia");
            Assert.NotEmpty(results);

        }

        [Fact]
        public void GetAll_FilterByMake_ReturnsListOfVehicles()
        {
            var results = _vehicleService.GetAll("Optima");
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetAll_FilterByYear_ReturnsListOfVehicles()
        {
            var results = _vehicleService.GetAll("2008");
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetAll_FilterByTrim_ReturnsListOfVehicles()
        {
            var results = _vehicleService.GetAll("Ex");
            Assert.NotEmpty(results);
        }
        
        [Fact]
        public void GetAll_FilterBySalePrice_ReturnsListOfVehicles()
        {
            var results = _vehicleService.GetAll("500");
            Assert.NotEmpty(results);
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
