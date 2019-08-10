using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Services;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class RepairServiceTests
    {
        private readonly RepairService _repairService;
        private readonly VehicleService _vehicleService;

        public RepairServiceTests()
        {
            var vehicleRepo = new VehicleRepositoryMock();
            _repairService = new RepairService(new RepairRepositoryMock(), vehicleRepo);
            _vehicleService = new VehicleService(vehicleRepo);
        }

        [Fact]
        public void AddRepair_WhenCalled_UpdatesVehicleSalePriceAndReturnsNewlyCreatedRepair()
        {
            var vehicle = _vehicleService.GetVehicle(id: 2);
            var currentSalePrice = vehicle.SalePrice;

            var repair = new Repair { Description = "Wash", Details = "Washed and waxed car", Cost = 5, VehicleId = vehicle.Id,
                RepairDate = DateTime.Today };

            var result = _repairService.AddRepair(repair);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal((currentSalePrice + repair.Cost), vehicle.SalePrice);
        }

        [Fact]
        public void GetAllByVehicleId_WhenCalled_ReturnsListOfRepairsByVehicleId()
        {
            var results = _repairService.GetAllByVehicleId(vehicleId: 2);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetById_WhenFound_ReturnsRepair()
        {
            var result = _repairService.GetById(id: 1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetById_WhenNotFound_ReturnsNull()
        {
            var result = _repairService.GetById(id: 9999);

            Assert.Null(result);
        }


        [Fact]
        public void UpdateRepair_WhenFound_UpdatesVehicleSalePriceAndReturnsUpdatedRepair()
        {
            var vehicle = _vehicleService.GetVehicle(id: 1);
            var currentSalePrice = vehicle.SalePrice;

            var repair = _repairService.GetById(id: 1);
            repair.Cost = 10;

            var result = _repairService.UpdateRepair(repair);

            Assert.NotNull(result);
            Assert.Equal((currentSalePrice + result.Cost), vehicle.SalePrice);
            Assert.NotEqual(currentSalePrice, vehicle.SalePrice);
        }
    }
}
