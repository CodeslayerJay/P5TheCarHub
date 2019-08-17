using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IRepairService
    {
        Repair SaveRepair(Repair repair);
        void DeleteRepair(int id);
        IEnumerable<Repair> GetAllByVehicleId(int vehicleId);
        Repair GetRepair(int id);
        
    }
}