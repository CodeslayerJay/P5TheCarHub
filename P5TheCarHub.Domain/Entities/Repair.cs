using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Entities
{
    public class Repair : BaseEntity
    {
        
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public DateTime? RepairDate { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
