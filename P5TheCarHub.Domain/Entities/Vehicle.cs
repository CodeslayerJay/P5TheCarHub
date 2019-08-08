using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        
        public string FullVehicleName { get; set; }
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public bool IsSold { get; set; } = false;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
