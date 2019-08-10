using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class InvoiceRepositoryMock : IInvoiceRepository
    {
        private List<Invoice> _context;

        public InvoiceRepositoryMock()
        {
            _context = new List<Invoice>();
            SetupFakeData();
        }

        private void SetupFakeData()
        {
            var seedData = new List<Invoice>()
            {
                new Invoice { Id = 1, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 1, InvoiceNumber = "TCH-V1"},
                new Invoice { Id = 2, CustomerName = "Jane Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 2, InvoiceNumber = "TCH-V2"},
            };

            _context.AddRange(seedData);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Invoice Add(Invoice entity)
        {
            if (entity.Id == 0)
                entity.Id = SetId();

            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var invoice = _context.SingleOrDefault(x => x.Id == id);

            if (invoice != null)
                _context.Remove(invoice);
        }

        public Invoice GetByVehicleId(int vehicleId)
        {
            return _context.Where(x => x.VehicleId == vehicleId).FirstOrDefault();
        }


        public ICollection<Invoice> GetAll()
        {
            return _context.ToList();
        }

        public Invoice GetById(int id)
        {
            return _context.SingleOrDefault(x => x.Id == id);
        }
    }
}
