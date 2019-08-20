using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using P5TheCarHub.Core.Filters;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Vehicle GetByVin(string vin, bool withIncludes);
        Vehicle GetById(int id, bool withIncludes);
        IEnumerable<Vehicle> GetAll(int? amount);
        IEnumerable<Vehicle> GetAll(VehicleFilter filter);
    }
}
