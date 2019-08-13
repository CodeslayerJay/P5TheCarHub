using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IInvoiceService
    {
        Invoice SaveInvoice(Invoice invoice);
        IEnumerable<Invoice> GetAll(int? amount = null);
        Invoice GetInvoice(int id);
        Invoice GetInvoiceByVehicleId(int vehicleId);
    }
}