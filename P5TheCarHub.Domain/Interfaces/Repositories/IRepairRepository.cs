using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IRepairRepository : IBaseRespository<Repair>
    {
        IEnumerable<Repair> GetAllByVehicleId(int vehicleId);
    }
}
