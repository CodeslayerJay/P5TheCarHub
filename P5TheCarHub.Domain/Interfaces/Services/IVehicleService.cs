using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        Vehicle AddVehicle(Vehicle vehicle);
        void DeleteVehicle(int id);
        IEnumerable<Vehicle> GetAll();
        Vehicle GetVehicle(int id);
        Vehicle GetVehicle(string vin);
        IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold);
        Vehicle UpdateVehicle(Vehicle vehicle);
        
    }
}