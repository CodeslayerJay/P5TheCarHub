using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class RepairService : IRepairService
    {
        private readonly IRepairRepository _repairRepo;
        private readonly IVehicleRepository _vehicleRepo;
        

        public RepairService(IRepairRepository repairRepository, IVehicleRepository vehicleRepository)
        {
            _repairRepo = repairRepository;
            _vehicleRepo = vehicleRepository;
             
        }

        public Repair AddRepair(Repair repair)
        {
            var vehicle = _vehicleRepo.GetById(repair.VehicleId);

            var spec = new VehicleExistsSpecification();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(repair.VehicleId);

            UpdateVehicleSalePrice(vehicle, repair);

            return _repairRepo.Add(repair);
        }

        private void UpdateVehicleSalePrice(Vehicle vehicle, Repair repair)
        {
            if (vehicle == null)
                throw new VehicleNotFoundException(repair.VehicleId);

            vehicle.SalePrice += repair.Cost;
        }

        public IEnumerable<Repair> GetAllByVehicleId(int vehicleId)
        {
            return _repairRepo.GetAllByVehicleId(vehicleId);
        }

        public Repair GetById(int id)
        {
            var repair = _repairRepo.GetById(id);

            if (repair == null)
                throw new RepairNotFoundException(id);

            return repair;
        }

        public Repair UpdateRepair(Repair repair)
        {

            var vehicle = _vehicleRepo.GetById(repair.VehicleId);

            var spec = new VehicleExistsSpecification();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(repair.VehicleId);
            
            var repairToUpdate = GetById(repair.Id);

            if (repairToUpdate == null)
                throw new RepairNotFoundException(repair.Id);

            UpdateVehicleSalePrice(vehicle, repair);

            _repairRepo.Update();

            return repair;
        }

        public void DeleteRepair(int id)
        {
            var repair = _repairRepo.GetById(id);

            if (repair == null)
                throw new RepairNotFoundException(id);

            _repairRepo.Delete(id);
            
        }
    }
}
