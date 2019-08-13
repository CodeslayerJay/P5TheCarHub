using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public RepairService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public Repair SaveRepair(Repair repair)
        {
            var vehicle = _unitOfWork.Vehicles.GetById(repair.VehicleId);

            var spec = new VehicleExistsSpecification();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(repair.VehicleId);

            UpdateVehicleSalePrice(vehicle, repair);

            if(repair.Id == 0)
                _unitOfWork.Repairs.Add(repair);

            _unitOfWork.SaveChanges();

            return repair;
        }

        private void UpdateVehicleSalePrice(Vehicle vehicle, Repair repair)
        {
            if (vehicle == null)
                throw new VehicleNotFoundException(repair.VehicleId);

            vehicle.SalePrice += repair.Cost;
        }

        public IEnumerable<Repair> GetAllByVehicleId(int vehicleId)
        {
            return _unitOfWork.Repairs.GetAllByVehicleId(vehicleId);
        }

        public Repair GetById(int id)
        {
            var repair = _unitOfWork.Repairs.GetById(id);

            if (repair == null)
                throw new RepairNotFoundException(id);

            return repair;
        }

        
        public void DeleteRepair(int id)
        {
            var repair = _unitOfWork.Repairs.GetById(id);

            if (repair == null)
                throw new RepairNotFoundException(id);

            _unitOfWork.Repairs.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
