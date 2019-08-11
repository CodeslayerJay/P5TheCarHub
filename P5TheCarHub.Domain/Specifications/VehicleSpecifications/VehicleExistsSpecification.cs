using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Specifications.VehicleSpecifications
{
    public class VehicleExistsSpecification : ISpecification<Vehicle>
    {
        
        public bool IsSatisfiedBy(Vehicle vehicle)
        {
            return (vehicle != null) ? true : false;
        }
    }
}
