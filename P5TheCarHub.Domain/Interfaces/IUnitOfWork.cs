using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P5TheCarHub.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInvoiceRepository Invoices { get; set; }
        IPhotoRepository Photos { get; set; }
        IRepairRepository Repairs { get; set; }
        IVehicleRepository Vehicles { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
