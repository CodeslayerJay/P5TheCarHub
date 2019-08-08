﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Domain.Entities
{
    public class Repair : BaseEntity
    {
        
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Details { get; set; }
        public DateTime RepairDate { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}