using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Vehicle GetByVin(string vin, bool withIncludes);
        Vehicle GetById(int id, bool withIncludes);
        IEnumerable<Vehicle> GetAll(int? amount, string orderBy);
    }
}
