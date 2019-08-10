using System;
using System.Runtime.Serialization;

namespace P5TheCarHub.Core.Exceptions
{
    public class VehicleNotGreaterThanRequiredYearException : Exception
    {

        public VehicleNotGreaterThanRequiredYearException(int requiredYear) :
            base($"Vehicle year be must greater than {requiredYear}")
        { }

    }
}