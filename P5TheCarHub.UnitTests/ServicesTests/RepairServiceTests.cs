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
    }
}
