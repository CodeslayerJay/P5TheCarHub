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
    }
}
