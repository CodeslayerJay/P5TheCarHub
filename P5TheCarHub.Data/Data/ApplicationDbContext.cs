using Microsoft.EntityFrameworkCore;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Infrastructure.Data.DbConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Database Table Configuration
            var dbConfiguation = new DbConfiguation();
            builder.Entity<Vehicle>(dbConfiguation.ConfigureVehicle);
            builder.Entity<Repair>(dbConfiguation.ConfigureRepair);
            builder.Entity<Photo>(dbConfiguation.ConfigurePhoto);
            builder.Entity<Invoice>(dbConfiguation.ConfigureInvoice);

            // Used for Identity
            base.OnModelCreating(builder);
        }
    }
}
