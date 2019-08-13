using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        Vehicle SaveVehicle(Vehicle vehicle);
        void DeleteVehicle(int id);
        IEnumerable<Vehicle> GetAll(int? amount = null);
        Vehicle GetVehicle(int id);
        Vehicle GetVehicleByVin(string vin);
        IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold);
        
    }
}