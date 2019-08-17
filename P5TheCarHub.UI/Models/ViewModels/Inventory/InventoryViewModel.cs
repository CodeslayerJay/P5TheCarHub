
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class InventoryViewModel
    {
        public InventoryViewModel()
        {
            Vehicles = new List<VehicleViewModel>();
        }

        public IEnumerable<VehicleViewModel> Vehicles { get; set; }
        public Pagination Pagination { get; set; }
        public bool IsFilterApplied { get; set; }
    }
}
