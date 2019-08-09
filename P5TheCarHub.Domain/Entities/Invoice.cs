using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Entities
{
    public class Invoice : BaseEntity
    {
        

        public string InvoiceNumber { get; set; }
        public decimal PriceSold { get; set; }
        public string CustomerName { get; set; }

        public DateTime DateSold { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
