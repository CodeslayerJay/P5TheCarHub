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

        public RepairServiceTests()
        {
            _repairService = new RepairService(new RepairRepositoryMock());
        }

        [Fact]
        public void AddRepair_WhenCalled_StoresAndReturnsNewlyCreatedRepair()
        {
            var repair = new Repair { Description = "Wash", Details = "Washed and waxed car", Cost = 5, VehicleId = 2, RepairDate = DateTime.Today };
            var result = _repairService.AddRepair(repair);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
        }
    }
}
