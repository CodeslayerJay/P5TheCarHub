using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class InvoiceAlreadyExistsForVehicleException : Exception
    {
        public InvoiceAlreadyExistsForVehicleException(int vehicleId) :
            base($"Vehicle id: {vehicleId} already has an invoice. Update or Remove current Invoice.")
        { }

    }
}
