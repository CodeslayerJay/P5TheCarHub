using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Specifications.VehicleSpecifications
{
    public class VehicleIsGreaterThanRequiredYear : ISpecification<Vehicle>
    {
        private int? _year;

        public int RequiredYear
        {
            get
            {
                if (_year.HasValue)
                    return _year.Value;

                return 1990;
            }
        }
        
        public VehicleIsGreaterThanRequiredYear(int? year = null)
        {
            _year = year;
        }

        public bool IsSatisfiedBy(Vehicle candidate)
        {
            if (_year.HasValue)
                return (candidate.Year >= _year.Value);

            return (candidate.Year >= 1990);
        }

    }
}
