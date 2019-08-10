using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Specifications.VehicleSpecifications
{
    public class VehicleExistsSpecification : ISpecification<int>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleExistsSpecification(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public bool IsSatisfiedBy(int vehicleId)
        {
            var vehicle = _vehicleRepository.GetById(vehicleId);

            return (vehicle != null) ? true : false;
        }
    }
}
