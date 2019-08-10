using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Entities
{
    public class Photo : BaseEntity
    {
        public int VehicleId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; } = false;

        public Vehicle Vehicle { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
