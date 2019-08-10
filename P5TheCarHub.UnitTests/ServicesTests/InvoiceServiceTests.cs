using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Services;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class InvoiceServiceTests
    {
        private readonly VehicleRepositoryMock _vehicleRepo;
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _vehicleRepo = new VehicleRepositoryMock();
            _invoiceService = new InvoiceService(new InvoiceRepositoryMock(), _vehicleRepo);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsListOfInvoices()
        {
            var result = _invoiceService.GetAll();

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<Invoice>>(result);
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
        public void AddInvoice_WhenIsUniqueToVehicle_AddsAndReturnsNewlyCreatedInvoice()
        {
            var invoice = new Invoice { Id = 3, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 3, InvoiceNumber = "TCH-V3" };

            var result = _invoiceService.AddInvoice(invoice);

            Assert.NotNull(result);
            Assert.NotEqual("TCH-V3", result.InvoiceNumber);
        }

        [Fact]
        public void AddInvoice_WhenVehicleAlreadyHasInvoice_ThrowsInvoiceAlreadyExistsForVehicleException()
        {
            var invoice = new Invoice { CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 1, InvoiceNumber = "TCH-V3" };

            Assert.Throws<InvoiceAlreadyExistsForVehicleException>(() => _invoiceService.AddInvoice(invoice));

        }

        [Fact]
        public void AddInvoice_WhenVehicleDoesNotExist_ThrowsVehicleNotFoundException()
        {
            var invoice = new Invoice { Id = 3, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, InvoiceNumber = "TCH-V3" };

            Assert.Throws<VehicleNotFoundException>(() => _invoiceService.AddInvoice(invoice));
        }

        [Fact]
        public void UpdateInvoice_WhenCalled_UpdatesAndReturnsInvoice()
        {
            var invoice = _invoiceService.GetInvoice(id: 1);
            var orgNameValue = invoice.CustomerName;
            
            invoice.CustomerName = "New Guy";
            //invoice.VehicleId = 999;
            invoice.InvoiceNumber = "TEST";

            var result = _invoiceService.UpdateInvoice(invoice);

            Assert.NotNull(result);
            //Assert.NotEqual(orgNameValue, result.CustomerName);
            //Assert.NotEqual(999, result.VehicleId);
            //Assert.NotEqual("TEST", result.InvoiceNumber);
        }
                
    }
}
