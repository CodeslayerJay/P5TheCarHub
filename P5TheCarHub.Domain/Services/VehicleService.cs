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
    public class VehicleService : IVehicleService
    {
        private const decimal MARKUPFEE = 500M;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
             
        }

        public string GetFullVehicleName(Vehicle vehicle)
        {
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim).ToUpper();
        }

        private decimal CalculateVehicleSalePrice(decimal purchasePrice)
        {
            return purchasePrice + MARKUPFEE;
        }

        public Vehicle SaveVehicle(Vehicle vehicle)
        {

            var vehicleGreaterThanYearSpec = new VehicleIsGreaterThanRequiredYear();
            if (!vehicleGreaterThanYearSpec.IsSatisfiedBy(vehicle))
                throw new VehicleNotGreaterThanRequiredYearException(vehicleGreaterThanYearSpec.RequiredYear);

            var vehicleVinIsUniqueSpec = new VehicleVinIsUnique(_unitOfWork.Vehicles);
            if (!vehicleVinIsUniqueSpec.IsSatisfiedBy(vehicle))
                throw new DuplicateVehicleVinException(vehicle.VIN);

            vehicle.VIN = vehicle.VIN.ToUpper();
            vehicle.SalePrice = CalculateVehicleSalePrice(vehicle.PurchasePrice);


            if(vehicle.Id == 0)
            {
                _unitOfWork.Vehicles.Add(vehicle);
            }
            
            _unitOfWork.SaveChanges();

            return vehicle;
        }
        
        public Vehicle GetVehicle(int id)
        {
            return _unitOfWork.Vehicles.GetById(id);
        }

        public Vehicle GetVehicleByVin(string vin)
        {
            if (String.IsNullOrEmpty(vin))
                return null;

            return _unitOfWork.Vehicles.GetByVin(vin);
        }

        public IEnumerable<Vehicle> GetAll(int? amount = null)
        {
            if (amount.HasValue)
                return _unitOfWork.Vehicles.GetAll(amount);

            return _unitOfWork.Vehicles.GetAll();
        }

        public void DeleteVehicle(int id)
        {
            var vehicle = GetVehicle(id);

            if (vehicle == null)
                throw new VehicleNotFoundException(id);
            
            _unitOfWork.Vehicles.Delete(id);
            _unitOfWork.SaveChanges();
                    
        }

        public IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold)
        {
            return GetAll().Where(x => x.IsSold == isSold).ToList();
        }
    }
}
