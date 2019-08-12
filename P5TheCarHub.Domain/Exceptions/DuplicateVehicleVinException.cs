using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class DuplicateVehicleVinException : Exception
    {
        public DuplicateVehicleVinException(string vin) : base($"A vehicle with VIN: {vin} already exists. Vehicles cannot share VIN numbers.")
        { }
    }
}
