using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Core.Specifications
{
    public class UniqueInvoiceSpecification : ISpecification<Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public UniqueInvoiceSpecification(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public bool IsSatisfiedBy(Invoice candidate)
        {
            var count = _invoiceRepository.GetAll().Where(x => x.VehicleId == candidate.VehicleId).Count();
            return (count == 0);
        }
    }
}
