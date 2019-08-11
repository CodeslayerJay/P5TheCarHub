using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class UnitOfWorkMock : IUnitOfWork
    {

        public UnitOfWorkMock()
        {
            Invoices = new InvoiceRepositoryMock();
            Photos = new PhotoRepositoryMock();
            Repairs = new RepairRepositoryMock();
            Vehicles = new VehicleRepositoryMock();
        }

        public IInvoiceRepository Invoices { get; set; }
        public IPhotoRepository Photos { get; set; }
        public IRepairRepository Repairs { get; set; }
        public IVehicleRepository Vehicles { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
