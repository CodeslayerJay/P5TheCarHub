﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class RepairViewModel
    {
        public int RepairId { get; set; }
        public int VehicleId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public DateTime? RepairDate { get; set; }

    }
}
