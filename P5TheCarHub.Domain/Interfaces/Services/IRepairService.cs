using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IRepairService
    {
        Repair AddRepair(Repair repair);
        void DeleteRepair(int id);
        IEnumerable<Repair> GetAllByVehicleId(int vehicleId);
        Repair GetById(int id);
        Repair UpdateRepair(Repair repair);
    }
}