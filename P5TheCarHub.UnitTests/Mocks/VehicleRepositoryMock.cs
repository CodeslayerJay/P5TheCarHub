﻿using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Enums;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class VehicleRepositoryMock : IVehicleRepository
    {
        private List<Vehicle> _context;

        public VehicleRepositoryMock()
        {
            _context = new List<Vehicle>();
            SetupFakeData();
        }

        private void SetupFakeData()
        {
            var vehicleSeeds = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Year = 2001,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = "3,000",
                    VIN = "123xyz",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500
                },
                new Vehicle
                {
                    Id = 2,
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = "3,000",
                    VIN = "1234-1234",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500,
                    AvailableStatus = VehicleAvailabilityStatus.Sold
                },
                 new Vehicle
                {
                    Id = 3,
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = "30,000",
                    VIN = "555",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500,
                    AvailableStatus = VehicleAvailabilityStatus.Sold
                },
                 new Vehicle
                {
                    Id = 999,
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = "30,000",
                    VIN = "555",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500,
                    AvailableStatus = VehicleAvailabilityStatus.Sold
                }
            };

            _context.AddRange(vehicleSeeds);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Vehicle GetByVin(string vin, bool withIncludes = false)
        {
            return _context.Where(x => x.VIN.ToUpper() == vin.ToUpper()).SingleOrDefault();
        }

        public Vehicle Add(Vehicle entity)
        {
            if (entity.Id == 0)
                entity.Id = SetId();

            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var vehicle = _context.SingleOrDefault(x => x.Id == id);

            if (vehicle != null)
                _context.Remove(vehicle);
        }

        public IEnumerable<Vehicle> GetAll(int? amount)
        {
            if (amount != null)
                return _context.Take(amount.Value).ToList();

            return _context.ToList();
        }

        
        public Vehicle GetById(int id, bool withIncludes = false)
        {
            return _context.SingleOrDefault(x => x.Id == id);
        }

        public Vehicle GetById(int id)
        {
            return _context.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate, VehicleFilter filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetAll(VehicleFilter filter)
        {
            filter.Size = 10;
            return GetAll(filter.Size.Value);
        }
    }
}
