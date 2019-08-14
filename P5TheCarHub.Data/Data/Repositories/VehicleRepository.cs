using Microsoft.EntityFrameworkCore;
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

        public Vehicle GetByVin(string vin, bool withIncludes = false)
        {

            if(!withIncludes)
                return _context.Vehicles.Where(x => x.VIN.ToUpper() == vin.ToUpper()).SingleOrDefault();

            return _context.Vehicles.Where(x => x.VIN.ToUpper() == vin.ToUpper())
                .Include(x => x.Repairs)
                .Include(x => x.Photos)
                .Include(x => x.Invoice)
                .SingleOrDefault();
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


        public IEnumerable<Vehicle> GetAll(int? amount = null)
        {
            if (amount.HasValue)
                return _context.Vehicles.Where(x => x.Id > 0).Take(amount.Value).ToList();

            return _context.Vehicles.Where(x => x.Id > 0).ToList();
        }


        public Vehicle GetById(int id)
        {
            return _context.Vehicles.SingleOrDefault(x => x.Id == id);
        }

        public Vehicle GetById(int id, bool withIncludes)
        {
            if (!withIncludes)
                return _context.Vehicles.Where(x => x.Id == id).SingleOrDefault();

            return _context.Vehicles.Where(x => x.Id == id)
                .Include(x => x.Repairs)
                .Include(x => x.Photos)
                .Include(x => x.Invoice)
                .SingleOrDefault();
        }

        public IEnumerable<Vehicle> Find(Expression<Func<Vehicle, bool>> predicate)
        {
            return _context.Vehicles.Where(predicate);
        }
    }
}

