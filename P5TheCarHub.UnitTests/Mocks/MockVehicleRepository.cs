using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class MockVehicleRepository : IVehicleRepository
    {
        private List<Vehicle> _context;

        public MockVehicleRepository()
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
                    FullVehicleName = "2001 KIA OPTIMA EX",
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "123xyz",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500
                },
                new Vehicle
                {
                    Id = 2,
                    FullVehicleName = "2008 KIA OPTIMA EX",
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "1234-1234",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500,
                    IsSold = true
                }
            };

            _context.AddRange(vehicleSeeds);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Vehicle GetByVin(string vin)
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

        public ICollection<Vehicle> GetAll()
        {
            return _context;
        }

        public IEnumerable<Vehicle> GetAllByFilter(string filter)
        {
            filter = filter.ToUpper();
            var results = new List<Vehicle>();

            results.AddRange(_context.FindAll(x => x.FullVehicleName.ToUpper().Contains(filter)));

            if(!results.Any())
                results.AddRange(_context.FindAll(x => x.VIN.ToUpper().Contains(filter)));

            if (!results.Any())
                results.AddRange(_context.FindAll(x => x.Year.ToString().Contains(filter)));

            if (!results.Any())
                results.AddRange(_context.FindAll(x => x.Mileage.ToString().Contains(filter)));

            if (!results.Any())
                results.AddRange(_context.FindAll(x => x.SalePrice.ToString().Contains(filter)));

            return results;

        }

        public Vehicle GetById(int id)
        {
            return _context.SingleOrDefault(x => x.Id == id);
        }
    }
}
