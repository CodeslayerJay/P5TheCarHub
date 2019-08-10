using System.Collections.Generic;
using P5TheCarHub.Core.Entities;

namespace P5TheCarHub.Core.Interfaces.Services
{
    public interface IInvoiceService
    {
        Invoice AddInvoice(Invoice invoice);
        IEnumerable<Invoice> GetAll();
        Invoice GetInvoice(int id);
        Invoice GetInvoiceByVehicleId(int vehicleId);
        Invoice UpdateInvoice(Invoice invoice);
    }
}