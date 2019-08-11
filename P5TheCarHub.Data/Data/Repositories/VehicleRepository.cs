using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Vehicle GetByVin(string vin)
        {
            return _context.Vehicles.Where(x => x.VIN.ToUpper() == vin.ToUpper()).SingleOrDefault();
        }

        public Vehicle Add(Vehicle entity)
        {            
            _context.Vehicles.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(x => x.Id == id);

            if (vehicle != null)
                _context.Vehicles.Remove(vehicle);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _context.Vehicles.Where(x => x.Id > 0).ToList();
        }


        public Vehicle GetById(int id)
        {
            return _context.Vehicles.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate)
        {
            return _context.Vehicles.Where(predicate);
        }
    }
}

