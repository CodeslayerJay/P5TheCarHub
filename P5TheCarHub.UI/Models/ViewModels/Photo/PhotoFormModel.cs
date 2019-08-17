
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class PhotoFormModel
    {
        
        public int PhotoId { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; } = false;
        public int VehicleId { get; set; }
        public string FullVehicleName { get; set; }
    }
}
