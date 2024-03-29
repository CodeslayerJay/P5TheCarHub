﻿using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using P5TheCarHub.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P5TheCarHub.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext dbContext, IInvoiceRepository invoiceRepository,
            IPhotoRepository photoRepository, IRepairRepository repairRepository,
            IVehicleRepository vehicleRepository)
        {
            _context = dbContext;
            // USE This process for using with a console app without DI
            //Vehicles = new VehicleRepository(_context);
            //Invoices = new InvoiceRepository(_context);
            //Repairs = new RepairRepository(_context);
            //Photos = new PhotoRepository(_context);
            Invoices = invoiceRepository;
            Photos = photoRepository;
            Repairs = repairRepository;
            Vehicles = vehicleRepository;

        }

        public IInvoiceRepository Invoices { get; set; }
        public IPhotoRepository Photos { get; set; }
        public IRepairRepository Repairs { get; set; }
        public IVehicleRepository Vehicles { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
