using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Specifications.InvoiceSpecifications;
using P5TheCarHub.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P5TheCarHub.UnitTests.SpecificationTests
{
    public class InvoiceSpecificationTests
    {
        private readonly InvoiceRepositoryMock _invoiceRepo;

        public InvoiceSpecificationTests()
        {
            _invoiceRepo = new InvoiceRepositoryMock();
        }

        [Fact]
        public void UniqueInvoiceSpec_CreatingNewInvoice_WhenSatisfied_ReturnsTrue()
        {
            var invoice = new Invoice { VehicleId = 99 };

            var spec = new UniqueInvoiceSpecification(_invoiceRepo);

            var result = spec.IsSatisfiedBy(invoice);

            Assert.True(result);
        }

        [Fact]
        public void UniqueInvoiceSpec_CreatingNewInvoice_WhenNotSatisfied_ReturnsFalse()
        {
            var invoice = new Invoice { VehicleId = 1 };

            var spec = new UniqueInvoiceSpecification(_invoiceRepo);

            var result = spec.IsSatisfiedBy(invoice);

            Assert.False(result);
        }

    }
}
