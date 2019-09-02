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
            
            _vehicleService = new VehicleService(_unitOfWork);
            _repairService = new RepairService(_unitOfWork, _vehicleService);
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
