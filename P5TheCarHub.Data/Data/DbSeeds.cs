using Microsoft.EntityFrameworkCore;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data
{
    public class DbSeeds
    {
        private readonly ModelBuilder _builder;

        public DbSeeds(ModelBuilder builder)
        {
            _builder = builder;
            
        }

        public void ExecuteSeed()
        {
            SeedVehicles();
            SeedRepairs();
        }

        private void SeedVehicles()
        {
            _builder.Entity<Vehicle>().HasData(
                new Vehicle {
                    Id = 1,
                    Year = 1991,
                    Make = "Mazda",
                    Model = "Miata",
                    Trim = "LE",
                    PurchaseDate = new DateTime(2019, 01, 07),
                    PurchasePrice = 1800.00M,
                    LotDate = new DateTime(2019, 04, 07),
                    SalePrice = 9900.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Sold,
                    SaleDate = new DateTime(2019, 04, 08)
                },
                new Vehicle
                {
                    Id = 2,
                    Year = 2007,
                    Make = "Jeep",
                    Model = "Liberty",
                    Trim = "Sport",
                    PurchaseDate = new DateTime(2019, 04, 02),
                    PurchasePrice = 4500.00M,
                    LotDate = new DateTime(2019, 04, 07),
                    SalePrice = 5350.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Sold,
                    SaleDate = new DateTime(2019, 04, 09)
                },
                new Vehicle
                {
                    Id = 3,
                    Year = 2007,
                    Make = "Dodge",
                    Model = "Grand Caravan",
                    Trim = "Sport",
                    PurchaseDate = new DateTime(2019, 04, 04),
                    PurchasePrice = 1800.00M,
                    LotDate = new DateTime(2019, 04, 08),
                    SalePrice = 2990.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Available,
                },
                new Vehicle
                {
                    Id = 4,
                    Year = 2017,
                    Make = "Ford",
                    Model = "Explorer",
                    Trim = "XLT",
                    PurchaseDate = new DateTime(2019, 04, 05),
                    PurchasePrice = 24350.00M,
                    LotDate = new DateTime(2019, 04, 09),
                    SalePrice = 25950.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Available,
                },
                new Vehicle
                {
                    Id = 5,
                    Year = 2008,
                    Make = "Honda",
                    Model = "Civic",
                    Trim = "LX",
                    PurchaseDate = new DateTime(2019, 04, 06),
                    PurchasePrice = 4000.00M,
                    LotDate = new DateTime(2019, 04, 09),
                    SalePrice = 4975.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Sold,
                    SaleDate = new DateTime(2019, 04, 09)
                },
                new Vehicle
                {
                    Id = 6,
                    Year = 2016,
                    Make = "Volkswagon",
                    Model = "GTI",
                    Trim = "S",
                    PurchaseDate = new DateTime(2019, 04, 06),
                    PurchasePrice = 15250.00M,
                    LotDate = new DateTime(2019, 04, 10),
                    SalePrice = 16190.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Sold,
                    SaleDate = new DateTime(2019, 04, 12)
                },
                new Vehicle
                {
                    Id = 7,
                    Year = 2013,
                    Make = "Ford",
                    Model = "Edge",
                    Trim = "SEL",
                    PurchaseDate = new DateTime(2019, 04, 07),
                    PurchasePrice = 10990.00M,
                    LotDate = new DateTime(2019, 04, 11),
                    SalePrice = 12440.00M,
                    AvailableStatus = VehicleAvailabilityStatus.Sold,
                    SaleDate = new DateTime(2019, 04, 12)
                }
            );
        }

        private void SeedRepairs()
        {
            _builder.Entity<Repair>().HasData(
                new Repair { Id = 1, VehicleId = 1, Cost = 7600.00M, Description = "Full Restoration" },
                new Repair { Id = 2, VehicleId = 2, Cost = 350.00M, Description = "Front wheel bearings" },
                new Repair { Id = 3, VehicleId = 3, Cost = 690.00M, Description = "Radiator, brakes" },
                new Repair { Id = 4, VehicleId = 4, Cost = 1100.00M, Description = "Tires, brakes" },
                new Repair { Id = 5, VehicleId = 5, Cost = 475.00M, Description = "AC, Brakes" },
                new Repair { Id = 6, VehicleId = 6, Cost = 440.00M, Description = "Tires" },
                new Repair { Id = 7, VehicleId = 7, Cost = 950.00M, Description = "Tires, Brakes, AC" }
            );
        }
    }
}
