using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepo;
        private const decimal MARKUPFEE = 500M;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepo = vehicleRepository;
        }

        public string GetFullVehicleName(Vehicle vehicle)
        {
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim).ToUpper();
        }

        private decimal CalculateVehicleSalePrice(decimal purchasePrice)
        {
            return purchasePrice + MARKUPFEE;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {

            var spec = new VehicleIsGreaterThanRequiredYear();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotGreaterThanRequiredYearException(spec.RequiredYear);

            vehicle.SalePrice = CalculateVehicleSalePrice(vehicle.PurchasePrice);
            _vehicleRepo.Add(vehicle);
            
            return vehicle;
        }

        public Vehicle GetVehicle(int id)
        {
            return _vehicleRepo.GetById(id);
        }

        public Vehicle GetVehicle(string vin)
        {
            if (String.IsNullOrEmpty(vin))
                return null;

            return _vehicleRepo.GetByVin(vin);
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            
            var vehicleToUpdate = GetVehicle(vehicle.Id);

            if (vehicleToUpdate == null)
                throw new VehicleNotFoundException(vehicle.Id);

            vehicle.SalePrice = CalculateVehicleSalePrice(vehicle.PurchasePrice);

            //TODO: Change This
            _vehicleRepo.Delete(vehicle.Id);
            _vehicleRepo.Add(vehicle);

            return vehicle;
        }
        

        public IEnumerable<Vehicle> GetAll()
        {
            return _vehicleRepo.GetAll();
        }

        public void DeleteVehicle(int id)
        {
            var vehicle = GetVehicle(id);

            if (vehicle != null)
                _vehicleRepo.Delete(id);
        }

        public IEnumerable<string> ValidateModel()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold)
        {
            return GetAll().Where(x => x.IsSold == isSold).ToList();
        }
    }
}
