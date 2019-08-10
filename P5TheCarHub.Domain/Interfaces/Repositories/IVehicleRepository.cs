using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRespository<Vehicle>
    {
        Vehicle GetByVin(string vin);
    }
}
