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
    public class RepairServiceTests
    {
        private readonly UnitOfWorkMock _unitOfWork;
        private readonly RepairService _repairService;
        private readonly VehicleService _vehicleService;

        public RepairServiceTests()
        {
            _unitOfWork = new UnitOfWorkMock();
            _repairService = new RepairService(_unitOfWork);
            _vehicleService = new VehicleService(_unitOfWork);
        }

        [Fact]
        public void SaveRepair_WhenCalled_UpdatesVehicleSalePriceAndReturnsNewlyCreatedRepair()
        {
            var vehicle = _vehicleService.GetVehicle(id: 2);
            var currentSalePrice = vehicle.SalePrice;

            var repair = new Repair { Description = "Wash", Details = "Washed and waxed car", Cost = 5, VehicleId = vehicle.Id,
                RepairDate = DateTime.Today };

            var result = _repairService.SaveRepair(repair);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal((currentSalePrice + repair.Cost), vehicle.SalePrice);
        }

        [Fact]
        public void SaveRepair_WhenVehicleDoesNotExist_ThrowsVehicleNotFoundException()
        {
            var repair = new Repair
            {
                Description = "Wash",
                Details = "Washed and waxed car",
                Cost = 5,
                RepairDate = DateTime.Today
            };

            Assert.Throws<VehicleNotFoundException>(() => _repairService.SaveRepair(repair));
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
            var result = _repairService.GetRepair(id: 1);

            Assert.NotNull(result);
        }

        
        [Fact]
        public void SaveRepair_WhenRepairIsFound_AndRepairIdIsNotZero_UpdatesVehicleSalePriceAndReturnsUpdatedRepair()
        {
            var vehicle = _vehicleService.GetVehicle(id: 1);
            var currentSalePrice = vehicle.SalePrice;

            var repair = _repairService.GetRepair(id: 1);
            repair.Cost = 10;

            var result = _repairService.SaveRepair(repair);

            Assert.NotNull(result);
            Assert.Equal((currentSalePrice + result.Cost), vehicle.SalePrice);
            Assert.NotEqual(currentSalePrice, vehicle.SalePrice);
        }

        [Fact]
        public void DeleteRepair_WhenRepairIsFound_RemovesRepair()
        {
            var vehicleId = 1;
            var repair = new Repair
            {   
                Description = "Wash",
                Details = "Washed and waxed car",
                Cost = 5,
                VehicleId = vehicleId,
                RepairDate = DateTime.Today
            };

            var result = _repairService.SaveRepair(repair);
            var orgCount = _repairService.GetAllByVehicleId(vehicleId).Count();

            _repairService.DeleteRepair(result.Id);
            Assert.NotEqual(orgCount, _repairService.GetAllByVehicleId(vehicleId).Count());
        }
    }
}
