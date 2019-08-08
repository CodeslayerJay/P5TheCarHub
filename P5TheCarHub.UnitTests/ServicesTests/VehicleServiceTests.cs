using P5TheCarHub.Domain.Entities;
using P5TheCarHub.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class VehicleServiceTests
    {
        [Fact]
        public void AddVehicle_WhenCalled_AddsFullVehicleNameAndReturnsNewlyCreatedVehicle()
        {
            var vehicleService = new VehicleService();
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

            var result = vehicleService.AddVehicle(vehicle);
            
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal("2001 Kia Optima Ex", result.FullVehicleName);
        }


    }
}
