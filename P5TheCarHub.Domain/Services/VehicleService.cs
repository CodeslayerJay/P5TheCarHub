using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepo;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepo = vehicleRepository;
        }

        private string VehicleFullNameStringBuilder(Vehicle vehicle)
        {
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim).ToUpper();
            
        }

        private decimal CalculateSalePrice(decimal purchasePrice)
        {
            return purchasePrice + 500M;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            vehicle.FullVehicleName = VehicleFullNameStringBuilder(vehicle);
            vehicle.SalePrice = CalculateSalePrice(vehicle.PurchasePrice);
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

        private IEnumerable<Vehicle> GetAllByFilter(string filter)
        {
            return _vehicleRepo.GetAllByFilter(filter);
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            
            var vehicleToUpdate = GetVehicle(vehicle.Id);

            if (vehicleToUpdate == null)
                return null;
                //throw new VehicleNotFoundException();

            //TODO: Change This
            _vehicleRepo.Delete(vehicle.Id);
            _vehicleRepo.Add(vehicle);

            return vehicle;
        }
        

        public IEnumerable<Vehicle> GetAll(string filter = null)
        {
            if (!String.IsNullOrEmpty(filter))
                return GetAllByFilter(filter);

            return _vehicleRepo.GetAll().ToList();
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
