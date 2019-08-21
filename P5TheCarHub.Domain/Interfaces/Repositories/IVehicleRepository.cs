using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using P5TheCarHub.Core.Filters;
using System.Linq.Expressions;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Vehicle GetByVin(string vin, bool withIncludes);
        Vehicle GetById(int id, bool withIncludes);
        IEnumerable<Vehicle> GetAll(VehicleFilter filter);
        IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate, VehicleFilter filter = null);
    }
}
