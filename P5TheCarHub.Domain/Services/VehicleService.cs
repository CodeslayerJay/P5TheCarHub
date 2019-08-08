using P5TheCarHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.Domain.Services
{
    public class VehicleService
    {
        public List<Vehicle> _repo { get; set; }
        
        public VehicleService()
        {
            _repo = new List<Vehicle>();
            SeedFakeData();
        }

        private void SeedFakeData()
        {
            var vehicleSeeds = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Year = 2001,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "1234-1234",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500
                },
                new Vehicle
                {
                    Id = 2,
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "1234-1234",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500
                }
            };

            _repo.AddRange(vehicleSeeds);
        }

        private int SetId()
        {
            return _repo.Count() + 1;
        }

        private string VehicleFullNameStringBuilder(Vehicle vehicle)
        {
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim);
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = (vehicle.Id == 0) ? SetId() : vehicle.Id;
            vehicle.FullVehicleName = VehicleFullNameStringBuilder(vehicle);
            _repo.Add(vehicle);
            
            return vehicle;
        }

        public Vehicle GetVehicle(int id)
        {
            return _repo.Where(x => x.Id == id).SingleOrDefault();
        }

    }
}
