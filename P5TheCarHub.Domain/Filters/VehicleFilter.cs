using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Filters
{
    public class VehicleFilter
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Size { get; set; }
        public int? Skip { get; set; }
        public int? VehicleStatus { get; set; }
        public bool OrderByDescending { get; set; }
    }
}
