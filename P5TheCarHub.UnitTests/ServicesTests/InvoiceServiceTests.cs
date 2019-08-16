using Moq;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Services;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class InvoiceServiceTests
    {
        private readonly UnitOfWorkMock _unitOfWork;
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _unitOfWork = new UnitOfWorkMock();
            
            _invoiceService = new InvoiceService(_unitOfWork);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsListOfInvoices()
        {
            var result = _invoiceService.GetAll();

            Assert.NotEmpty(result);
            
        }

        [Fact]
        public void GetAll_WithAmountSet_ReturnsTotalListOfInvoicesBasedOnAmount()
        {
            var result = _invoiceService.GetAll(1);

            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void GetInvoice_WhenFound_ReturnsInvoice()
        {
            var result = _invoiceService.GetInvoice(id: 1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetByVehicleId_WhenCalled_ReturnsInvoice()
        {
            var result = _invoiceService.GetInvoiceByVehicleId(vehicleId: 1);

            Assert.NotNull(result);
        }

        [Fact]
        public void SaveInvoice_WhenIsUniqueToVehicle_AddsAndReturnsNewlyCreatedInvoice()
        {
            var invoice = new Invoice { Id = 3, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 3, InvoiceNumber = "TCH-V3" };

            var result = _invoiceService.SaveInvoice(invoice);

            Assert.NotNull(result);
            Assert.NotEqual("TCH-V3", result.InvoiceNumber);
        }

        [Fact]
        public void SaveInvoice_WhenVehicleAlreadyHasInvoice_ThrowsInvoiceAlreadyExistsForVehicleException()
        {
            var invoice = new Invoice { CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 1, InvoiceNumber = "TCH-V3" };

            Assert.Throws<InvoiceAlreadyExistsForVehicleException>(() => _invoiceService.SaveInvoice(invoice));

        }

        [Fact]
        public void SaveInvoice_WhenVehicleDoesNotExist_ThrowsVehicleNotFoundException()
        {
            var invoice = new Invoice { Id = 3, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, InvoiceNumber = "TCH-V3" };

            Assert.Throws<VehicleNotFoundException>(() => _invoiceService.SaveInvoice(invoice));
        }

        [Fact]
        public void SaveInvoice_WhenInvoiceIdNotZero_UpdatesAndReturnsInvoice()
        {
            var invoice = _invoiceService.GetInvoice(id: 1);
            var orgNameValue = invoice.CustomerName;
            
            invoice.CustomerName = "New Guy";
            
            invoice.InvoiceNumber = "TEST";

            var result = _invoiceService.SaveInvoice(invoice);

            Assert.NotNull(result);
        }
             
        [Fact]
        public void DeleteInvoice_WhenFound_DeletesInvoice()
        {
            var invoiceToDelete = new Invoice { VehicleId = 999, PriceSold = 3000M };
            var result = _invoiceService.SaveInvoice(invoiceToDelete);

            _invoiceService.DeleteInvoice(invoiceToDelete.Id);

            Assert.Null(_invoiceService.GetInvoice(invoiceToDelete.Id));

        }

        [Fact]
        public void DeleteInvoice_WhenNotFound_ThrowsInvoiceNotFoundException()
        {
            Assert.Throws<InvoiceNotFoundException>(() => _invoiceService.DeleteInvoice(id: 999));

        }
    }
}
