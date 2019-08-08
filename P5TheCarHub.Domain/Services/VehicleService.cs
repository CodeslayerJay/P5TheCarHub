using P5TheCarHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Domain.Services
{
    public class VehicleService
    {
        public List<Vehicle> VehiclesTemp { get; set; }
        
        public VehicleService()
        {
            VehiclesTemp = new List<Vehicle>();            
        }

        private int SetId()
        {
            return VehiclesTemp.Count() + 1;
        }

        private string VehicleFullNameStringBuilder(Vehicle vehicle)
        {
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim);
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = (vehicle.Id == 0) ? SetId() : vehicle.Id;
            vehicle.FullVehicleName = VehicleFullNameStringBuilder(vehicle);
            VehiclesTemp.Add(vehicle);
            
            return vehicle;
        }

    }
}
