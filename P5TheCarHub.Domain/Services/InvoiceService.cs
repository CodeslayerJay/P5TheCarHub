using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Specifications.InvoiceSpecifications;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IVehicleRepository _vehicleRepo;
        private const string APP_NAME_INITIALS = "TCH";

        public InvoiceService(IInvoiceRepository invoiceRepository, IVehicleRepository vehicleRepository)
        {
            _invoiceRepo = invoiceRepository;
            _vehicleRepo = vehicleRepository;
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
            var spec = new VehicleExistsSpecification(_vehicleRepo);
            if (!spec.IsSatisfiedBy(invoice.VehicleId))
                throw new VehicleNotFoundException(invoice.VehicleId);

            if(!CheckInvoiceIsUniqueToVehicle(invoice))
                throw new InvoiceAlreadyExistsForVehicleException(invoice.VehicleId);

            invoice.InvoiceNumber = GenerateInvoiceNumber(invoice.VehicleId);
            return _invoiceRepo.Add(invoice);
        }

        private string GenerateInvoiceNumber(int vehicleId)
        {
            var invoiceCount = _invoiceRepo.GetAll().Count() + 1;
            return $"{APP_NAME_INITIALS}-V{vehicleId}I{invoiceCount}";
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            var invoiceToUpdate = GetInvoice(invoice.Id);

            if (invoiceToUpdate == null)
                throw new InvoiceNotFoundException(invoice.Id);
                
            //if(!CheckInvoiceIsUniqueToVehicle(invoice))
            //    throw new InvoiceAlreadyExistsForVehicleException(invoice.VehicleId);

            return invoiceToUpdate;
        }

        private bool CheckInvoiceIsUniqueToVehicle(Invoice invoice)
        {
            var spec = new UniqueInvoiceSpecification(_invoiceRepo);
            return spec.IsSatisfiedBy(invoice);
        }

    }
}
