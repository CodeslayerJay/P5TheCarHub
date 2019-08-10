using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class RepairNotFoundException : Exception
    {
        public RepairNotFoundException(int repairId) : base($"Repair was not found with id: {repairId}")
        { }
    }
}
