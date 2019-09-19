using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Enums;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.Core.Specifications.InvoiceSpecifications;
using P5TheCarHub.Core.Specifications.VehicleSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private const string APP_NAME_INITIALS = "TCH";

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public IEnumerable<Invoice> GetAll(int? amount = null)
        {
            return _unitOfWork.Invoices.GetAll(amount);
        }

        public Invoice GetInvoice(int id)
        {
            return _unitOfWork.Invoices.GetById(id);
        }

        public Invoice GetInvoiceByVehicleId(int vehicleId)
        {
            return _unitOfWork.Invoices.GetByVehicleId(vehicleId);
        }

        public Invoice SaveInvoice(Invoice invoice)
        {

            var vehicle = _unitOfWork.Vehicles.GetById(invoice.VehicleId);

            var vehicleExistsSpec = new VehicleExistsSpecification();
            if (!vehicleExistsSpec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(invoice.VehicleId);

            if(invoice.Id == 0 || invoice.VehicleId != vehicle.Id)
            {
                var uniqueInvoiceSpec = new UniqueInvoiceSpecification(_unitOfWork.Invoices);
                if (!uniqueInvoiceSpec.IsSatisfiedBy(invoice))
                    throw new InvoiceAlreadyExistsForVehicleException(invoice.VehicleId);
            }
            
            SetVehicleToSoldStatus(vehicle);

            if (invoice.Id == 0)
            {
                invoice.InvoiceNumber = GenerateInvoiceNumber(invoice.VehicleId);
                _unitOfWork.Invoices.Add(invoice);
            }

            _unitOfWork.SaveChanges();

            return invoice;
        }

        private void UpdateInvoice(Invoice invoice)
        {
            var invoiceToUpdate = _unitOfWork.Invoices.GetById(invoice.Id);

            if (invoiceToUpdate == null)
                throw new InvoiceNotFoundException(invoice.Id);

            _unitOfWork.SaveChanges();
        }

        private void SetVehicleToSoldStatus(Vehicle vehicle)
        {
            vehicle.AvailableStatus = VehicleAvailabilityStatus.Sold;
        }

        private string GenerateInvoiceNumber(int vehicleId)
        {
            var invoiceCount = _unitOfWork.Invoices.GetAll().Count() + 1;
            return $"{APP_NAME_INITIALS}-V{vehicleId}I{invoiceCount}";
        }

        private bool CheckInvoiceIsUniqueToVehicle(Invoice invoice)
        {
            var spec = new UniqueInvoiceSpecification(_unitOfWork.Invoices);
            return spec.IsSatisfiedBy(invoice);
        }

        public void DeleteInvoice(int id)
        {
            var invoice = GetInvoice(id);

            if (invoice == null)
                throw new InvoiceNotFoundException(id);

            var vehicle = _unitOfWork.Vehicles.GetById(invoice.VehicleId);

            if(vehicle != null)
            {
                vehicle.AvailableStatus = VehicleAvailabilityStatus.NotAvailable;
            }

            _unitOfWork.Invoices.Delete(id);
            _unitOfWork.SaveChanges();
        }

    }
}
