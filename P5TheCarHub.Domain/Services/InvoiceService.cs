using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepo = invoiceRepository;
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _invoiceRepo.GetAll();
        }

        public Invoice GetInvoice(int id)
        {
            return _invoiceRepo.GetById(id);
        }

        public Invoice GetInvoiceByVehicleId(int vehicleId)
        {
            return _invoiceRepo.GetByVehicleId(vehicleId);
        }

        public Invoice AddInvoice(Invoice invoice)
        {
            //TODO: Apply business rules: Unique Invoice Number, must be auto-generated. Vehicle can only have 1 invoice
            return _invoiceRepo.Add(invoice);
        }

    }
}
