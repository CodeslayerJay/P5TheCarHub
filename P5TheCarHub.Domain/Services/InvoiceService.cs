using P5TheCarHub.Core.Entities;
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

        public Invoice AddInvoice(Invoice invoice)
        {

            var vehicle = _unitOfWork.Vehicles.GetById(invoice.VehicleId);

            var spec = new VehicleExistsSpecification();
            if (!spec.IsSatisfiedBy(vehicle))
                throw new VehicleNotFoundException(invoice.VehicleId);

            if(!CheckInvoiceIsUniqueToVehicle(invoice))
                throw new InvoiceAlreadyExistsForVehicleException(invoice.VehicleId);

            invoice.InvoiceNumber = GenerateInvoiceNumber(invoice.VehicleId);

            SetVehicleToSoldStatus(vehicle);

            var newInvoice = _unitOfWork.Invoices.Add(invoice);
            _unitOfWork.SaveChanges();

            return newInvoice;
        }

        private void SetVehicleToSoldStatus(Vehicle vehicle)
        {
            vehicle.IsSold = true;
        }

        private string GenerateInvoiceNumber(int vehicleId)
        {
            var invoiceCount = _unitOfWork.Invoices.GetAll().Count() + 1;
            return $"{APP_NAME_INITIALS}-V{vehicleId}I{invoiceCount}";
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            var invoiceToUpdate = GetInvoice(invoice.Id);

            if (invoiceToUpdate == null)
                throw new InvoiceNotFoundException(invoice.Id);

            if(invoice.VehicleId != invoiceToUpdate.VehicleId)
            {
                if (!CheckInvoiceIsUniqueToVehicle(invoice))
                    throw new InvoiceAlreadyExistsForVehicleException(invoice.VehicleId);
            }
            
            _unitOfWork.SaveChanges();
            return invoiceToUpdate;
        }

        private bool CheckInvoiceIsUniqueToVehicle(Invoice invoice)
        {
            var spec = new UniqueInvoiceSpecification(_unitOfWork.Invoices);
            return spec.IsSatisfiedBy(invoice);
        }

    }
}
