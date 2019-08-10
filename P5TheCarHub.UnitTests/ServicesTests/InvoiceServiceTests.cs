using P5TheCarHub.Core.Entities;
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
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {            
            _invoiceService = new InvoiceService(new MockInvoiceRepository());
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
        public void AddInvoice_WhenCalled_AddsAndReturnsNewlyCreatedInvoice()
        {
            var invoice = new Invoice { Id = 3, CustomerName = "John Doe", DateSold = DateTime.Now, PriceSold = 3000, VehicleId = 3, InvoiceNumber = "TCH-V3" };

            var result = _invoiceService.AddInvoice(invoice);

            Assert.NotNull(result);
        }
    }
}
