using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Specifications.VehicleSpecifications
{
    public class VehicleVinIsUnique : ISpecification<Vehicle>
    {
        
        private readonly IVehicleRepository _vehicleRepo;

        public VehicleVinIsUnique(IVehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        public bool IsSatisfiedBy(Vehicle candidate)
        {
            if (candidate.VIN == null)
                return true;

            var vehicle = _vehicleRepo.GetByVin(candidate.VIN.ToUpper(), withIncludes: false);

            if (vehicle != null && vehicle?.Id != candidate.Id)
                return false;

            
            return true;
        }
    }
}
