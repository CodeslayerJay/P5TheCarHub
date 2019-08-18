using P5TheCarHub.Core.Enums;
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
        public Pagination Pagination { get; set; }
        public VehicleAvailabilityStatus VehicleStatus { get; set; }
    }
}
