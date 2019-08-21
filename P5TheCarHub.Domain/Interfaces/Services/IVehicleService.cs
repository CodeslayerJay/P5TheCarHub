using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Filters;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IVehicleService
    {
        Vehicle SaveVehicle(Vehicle vehicle);
        void DeleteVehicle(int id);
        IEnumerable<Vehicle> GetAll(int? amount = null);
        Vehicle GetVehicle(int id, bool withIncludes);
        Vehicle GetVehicleByVin(string vin, bool withIncludes);
        IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold);
        IEnumerable<Vehicle> GetAll(VehicleFilter filter);
        string GetVehicleFullName(Vehicle vehicle);
        IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate, VehicleFilter filter = null);
    }
}