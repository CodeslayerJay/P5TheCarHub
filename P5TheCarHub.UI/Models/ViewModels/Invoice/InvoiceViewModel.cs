
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public int VehicleId { get; set; }
        public decimal PriceSold { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DateSold { get; set; }
        public string InvoiceNumber { get; set; }

        public VehicleViewModel Vehicle { get; set; }
    }
}
