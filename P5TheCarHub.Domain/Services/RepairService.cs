using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class RepairService : IRepairService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleService _vehicleService;

        public RepairService(IUnitOfWork unitOfWork, IVehicleService vehicleService)
        {
            _unitOfWork = unitOfWork;
            _vehicleService = vehicleService;
        }

        public Repair SaveRepair(Repair repair)
        {
            var vehicle = _unitOfWork.Vehicles.GetById(repair.VehicleId);

            var spec = new VehicleExistsSpecification();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(repair.VehicleId);

            if(repair.Id == 0)
                _unitOfWork.Repairs.Add(repair);

            _unitOfWork.SaveChanges();

            _vehicleService.UpdateSalePrice(vehicle.Id);
            return repair;
        }
        
        public IEnumerable<Repair> GetAllByVehicleId(int vehicleId)
        {
            return _unitOfWork.Repairs.GetAllByVehicleId(vehicleId);
        }

        public Repair GetRepair(int id)
        {
            return _unitOfWork.Repairs.GetById(id);
        }

        
        public void DeleteRepair(int id)
        {
            var repair = _unitOfWork.Repairs.GetById(id);

            if (repair == null)
                throw new RepairNotFoundException(id);

            var vehicle = _unitOfWork.Vehicles.GetById(repair.VehicleId);

            _unitOfWork.Repairs.Delete(id);
            _unitOfWork.SaveChanges();

            if(vehicle != null)
            {
                _vehicleService.UpdateSalePrice(vehicle.Id);
                
            }
                
        }
    }
}
