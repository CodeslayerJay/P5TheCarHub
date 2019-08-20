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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Invoice Add(Invoice entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var invoice = _context.Invoices.SingleOrDefault(x => x.Id == id);

            if (invoice != null)
                _context.Remove(invoice);
        }

        public Invoice GetByVehicleId(int vehicleId)
        {
            return _context.Invoices.Include(x => x.Vehicle).Where(x => x.VehicleId == vehicleId).FirstOrDefault();
        }


        public IEnumerable<Invoice> GetAll(int? amount = null)
        {
            if (amount.HasValue)
                return _context.Invoices.Where(x => x.Id > 0).Include(x => x.Vehicle).Take(amount.Value).ToList();

            return _context.Invoices.Where(x => x.Id > 0).Include(x => x.Vehicle).ToList();
        }

        public Invoice GetById(int id)
        {
            return _context.Invoices.Include(x => x.Vehicle).SingleOrDefault(x => x.Id == id);
        }


        public IEnumerable<Invoice> Find(Expression<Func<Invoice, bool>> predicate)
        {
            return _context.Invoices.Where(predicate);
        }
    }
}
