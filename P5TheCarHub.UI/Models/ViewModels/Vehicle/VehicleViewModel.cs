

using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class VehicleViewModel
    {

        public VehicleViewModel()
        {
            Repairs = new List<RepairViewModel>();
            Photos = new List<PhotoViewModel>();
        }

        public int VehicleId { get; set; }
        public string FullVehicleName { get; set; } 
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string VIN { get; set; }
        public string Mileage { get; set; }
        public string Color { get; set; }
        public bool IsSold { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LotDate { get; set; }

        public decimal SalePrice { get; set; }

        public PhotoViewModel MainPhoto => GetMainPhoto();

        public IEnumerable<RepairViewModel> Repairs { get; set; }
        public IEnumerable<PhotoViewModel> Photos { get; set; }

        public InvoiceViewModel Invoice { get; set; }

        public decimal TotalRepairCosts => CalculateTotalRepairCosts();

        private decimal CalculateTotalRepairCosts()
        {
            var costs = Repairs.Any() ? Repairs.Sum(x => x.Cost) : 0.0M;
            return costs;
        }

        private PhotoViewModel GetMainPhoto()
        {
            return Photos.FirstOrDefault(x => x.IsMain == true);
        }

    }
}
