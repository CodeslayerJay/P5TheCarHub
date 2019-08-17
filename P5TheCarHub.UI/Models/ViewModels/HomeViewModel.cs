﻿
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
            Vehicles = new HashSet<VehicleViewModel>();
        }
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }
    }
}
