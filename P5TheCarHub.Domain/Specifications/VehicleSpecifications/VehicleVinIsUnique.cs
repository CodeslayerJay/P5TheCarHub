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
        
        private readonly IUnitOfWork _unitOfwork;

        public VehicleVinIsUnique(IUnitOfWork unitOfwork)
        {
            
            _unitOfwork = unitOfwork;
        }

        public bool IsSatisfiedBy(Vehicle candidate)
        {

            var vehicle = _unitOfwork.Vehicles.GetByVin(candidate.VIN.ToUpper());

            if (vehicle != null && vehicle?.Id != candidate.Id)
                return false;

            
            return true;
        }
    }
}
