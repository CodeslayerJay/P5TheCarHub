using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class RepairFormModel
    {
        public int RepairId { get; set; }
        public int VehicleId { get; set; }

        public string VehicleFullName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public DateTime? RepairDate { get; set; }

        public bool AddAnotherRepair { get; set; }
        public string ReturnUrl { get; set; } = "/manage/vehicles";
    }
}
