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
                    FullVehicleName = "2001 KIA OPTIMA EX",
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "123xyz",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500
                },
                new Vehicle
                {
                    Id = 2,
                    FullVehicleName = "2008 KIA OPTIMA EX",
                    Year = 2008,
                    Make = "Kia",
                    Model = "Optima",
                    Trim = "Ex",
                    Mileage = 30000,
                    VIN = "1234-1234",
                    LotDate = DateTime.Today,
                    PurchaseDate = DateTime.Today,
                    PurchasePrice = 3000M,
                    SalePrice = 500,
                    IsSold = true
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
            return String.Concat(vehicle.Year, " ", vehicle.Make, " ", vehicle.Model, " ", vehicle.Trim).ToUpper();
            
        }

        private decimal CalculateSalePrice(decimal purchasePrice)
        {
            return purchasePrice + 500M;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = (vehicle.Id == 0) ? SetId() : vehicle.Id;
            vehicle.FullVehicleName = VehicleFullNameStringBuilder(vehicle);
            vehicle.SalePrice = CalculateSalePrice(vehicle.PurchasePrice);
            _repo.Add(vehicle);
            
            return vehicle;
        }

        public Vehicle GetVehicle(int id)
        {
            return _repo.Where(x => x.Id == id).SingleOrDefault();
        }

        public Vehicle GetVehicle(string vin)
        {
            if (String.IsNullOrEmpty(vin))
                return null;

            return _repo.Where(x => x.VIN.ToUpper() == vin.ToUpper()).SingleOrDefault();
        }

        public IEnumerable<Vehicle> GetAllByMake(string make)
        {
            return _repo.Where(x => x.Make.ToUpper() == make.ToUpper()).ToList();
        }

        public IEnumerable<Vehicle> GetAllByModel(string model)
        {
            return _repo.Where(x => x.Model.ToUpper() == model.ToUpper()).ToList();
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            
            var vehicleToUpdate = GetVehicle(vehicle.Id);

            if (vehicleToUpdate == null)
                return null;

            _repo.Remove(vehicleToUpdate);
            _repo.Add(vehicle);

            return vehicle;
        }

        public IEnumerable<Vehicle> GetAll(string filter = null)
        {
            if (!String.IsNullOrEmpty(filter))
                return _repo.Where(x => x.FullVehicleName.Contains(filter.ToUpper())).ToList();

            return _repo.ToList();
        }

        public void DeleteVehicle(int id)
        {
            var vehicle = GetVehicle(id);

            if (vehicle != null)
                _repo.Remove(vehicle);
        }

        public IEnumerable<string> ValidateModel()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetVehiclesBySoldStatus(bool isSold)
        {
            return GetAll().Where(x => x.IsSold == isSold).ToList();
        }
    }
}
