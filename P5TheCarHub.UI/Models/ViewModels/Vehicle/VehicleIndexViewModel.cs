using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class VehicleIndexViewModel
    {
        public VehicleIndexViewModel()
        {
            Vehicles = new HashSet<VehicleViewModel>();
        }

        public IEnumerable<VehicleViewModel> Vehicles { get; set; }
        public bool IsFilterApplied { get; set; }
    }
}
