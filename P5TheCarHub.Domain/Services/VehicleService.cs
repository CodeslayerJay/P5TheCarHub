﻿using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Enums;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        
        public string GetVehicleFullName(Vehicle vehicle)
        {
            return $"{vehicle.Year} {vehicle.Make} {vehicle.Model} {vehicle.Trim}";
        }

        public Vehicle SaveVehicle(Vehicle vehicle)
        {

            var vehicleGreaterThanYearSpec = new VehicleIsGreaterThanRequiredYear();
            if (!vehicleGreaterThanYearSpec.IsSatisfiedBy(vehicle))
                throw new VehicleNotGreaterThanRequiredYearException(vehicleGreaterThanYearSpec.RequiredYear);

            var vehicleVinIsUniqueSpec = new VehicleVinIsUnique(_unitOfWork.Vehicles);
            if (!vehicleVinIsUniqueSpec.IsSatisfiedBy(vehicle))
                throw new DuplicateVehicleVinException(vehicle.VIN);

            vehicle.VIN = vehicle.VIN?.ToUpper();
            
            if(vehicle.Id == 0)
            {
                _unitOfWork.Vehicles.Add(vehicle);
            }
           
            _unitOfWork.SaveChanges();

            UpdateSalePrice(vehicle.Id);
            return vehicle;
        }

        public void UpdateSalePrice(int vehicleId)
        {
            var vehicle = _unitOfWork.Vehicles.GetById(vehicleId);

            if(vehicle != null)
            {
                var repairs = _unitOfWork.Repairs.GetAllByVehicleId(vehicle.Id);

                if (repairs.Any())
                {
                    vehicle.SalePrice = vehicle.PurchasePrice + MARKUPFEE + repairs.Sum(x => x.Cost);
                } else
                {
                    vehicle.SalePrice = vehicle.PurchasePrice + MARKUPFEE;
                }

                _unitOfWork.SaveChanges();
            }
        }

        
        public Vehicle GetVehicle(int id, bool withIncludes = false)
        {
            return _unitOfWork.Vehicles.GetById(id, withIncludes);
        }

        public Vehicle GetVehicleByVin(string vin, bool withIncludes = false)
        {
            if (String.IsNullOrEmpty(vin))
                return null;

            return _unitOfWork.Vehicles.GetByVin(vin, withIncludes);
        }

        public IEnumerable<Vehicle> GetAll(int? amount = null)
        {
            return _unitOfWork.Vehicles.GetAll(amount);
        }

        public IEnumerable<Vehicle> GetAll(VehicleFilter filter)
        {
            return _unitOfWork.Vehicles.GetAll(filter);
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
            return GetAll().Where(x => x.AvailableStatus == VehicleAvailabilityStatus.Sold).ToList();
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate, VehicleFilter filter = null)
        {
            return _unitOfWork.Vehicles.Find(predicate, filter);
        }
    }
}
