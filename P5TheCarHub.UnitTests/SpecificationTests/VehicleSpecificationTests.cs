using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.SpecificationTests
{
    public class VehicleSpecificationTests
    {
        private readonly UnitOfWorkMock _unitOfWork;

        public VehicleSpecificationTests()
        {
            _unitOfWork = new UnitOfWorkMock();
        }

        [Fact]
        public void VehicleExistsSpecificationSpec_WhenNotSatisfied_ReturnsFalse()
        {
            Vehicle vehicle = null;

            var spec = new VehicleExistsSpecification();

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void VehicleExistsSpecificationSpec_WhenSatisfied_ReturnsTrue()
        {
            var vehicle = new Vehicle { Id = 1, };

            var spec = new VehicleExistsSpecification();

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void VehicleVinIsUniqueSpec_WhenNotSatisfied_ReturnsFalse()
        {
            var vehicle = new Vehicle { VIN = "1234-1234" };

            var spec = new VehicleVinIsUnique(_unitOfWork);

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.False(result);
        }

        [Fact]
        public void VehicleVinIsUniqueSpec_WhenSatisfied_ReturnsTrue()
        {
            var vehicle = new Vehicle { VIN = "1234-1234-NEW-TESTING" };

            var spec = new VehicleVinIsUnique(_unitOfWork);

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void VehicleVinIsUniqueSpec_WhenUpdatingVin_IsSatisfied_ReturnsTrue()
        {
            var vehicle = _unitOfWork.Vehicles.GetById(1);
            vehicle.VIN = "new vin number for tests";

            var spec = new VehicleVinIsUnique(_unitOfWork);

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void VehicleVinIsUniqueSpec_WhenUpdatingVehicleButNotVin_IsSatisfied_ReturnsTrue()
        {
            var vehicle = _unitOfWork.Vehicles.GetById(1);
            vehicle.Year = 2015;

            var spec = new VehicleVinIsUnique(_unitOfWork);

            var result = spec.IsSatisfiedBy(vehicle);

            Assert.True(result);
        }

        [Fact]
        public void VehicleIsGreaterThanRequiredYearSpec_WhenSatisfied_ReturnsTrue()
        {
            var vehicle = new Vehicle { Year = 2000 };
            var spec = new VehicleIsGreaterThanRequiredYear();
            var result = spec.IsSatisfiedBy(vehicle);

            Assert.True(result);
            
        }

        [Fact]
        public void VehicleIsGreaterThanRequiredYearSpec_WhenNotSatisfied_ReturnsFalse()
        {
            var vehicle = new Vehicle { Year = 1980 };
            var spec = new VehicleIsGreaterThanRequiredYear();
            var result = spec.IsSatisfiedBy(vehicle);

            Assert.False(result);

        }
    }
}
