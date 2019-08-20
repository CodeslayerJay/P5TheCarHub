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
    public class RepairRepository : IRepairRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Repair Add(Repair entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var repair = _context.Repairs.SingleOrDefault(x => x.Id == id);

            if (repair != null)
                _context.Remove(repair);
        }

        public IEnumerable<Repair> GetAll(int? amount = null)
        {
            if (amount.HasValue)
                return _context.Repairs.Where(x => x.Id > 0).Take(amount.Value).ToList();

            return _context.Repairs.Where(x => x.Id > 0).ToList();
        }

        public Repair GetById(int id)
        {
            return _context.Repairs.Include(x => x.Vehicle).Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<Repair> GetAllByVehicleId(int vehicleId)
        {
            return _context.Repairs.Where(x => x.VehicleId == vehicleId).ToList();
        }

        public IEnumerable<Repair> Find(Expression<Func<Repair, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
