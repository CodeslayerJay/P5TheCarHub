using P5CarSalesAppBasic.Models.Enums;
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
            Cars = new List<VehicleViewModel>();
        }

        public IEnumerable<VehicleViewModel> Cars { get; set; }
        public Dictionary<CarType, int> CarTypesAndQuantity { get; set; }

        
    }
}
