
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            VehiclesForSale = new HashSet<VehicleViewModel>();
            VehiclesSold = new HashSet<VehicleViewModel>();

        }
        public IEnumerable<VehicleViewModel> VehiclesForSale { get; set; }
        public IEnumerable<VehicleViewModel> VehiclesSold { get; set; }
        
    }
}
