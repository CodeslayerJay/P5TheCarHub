using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException(int vehicleId) : base($"Vehicle was not found with id: {vehicleId}")
        { }

    }
}
