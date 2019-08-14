
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.ViewModels
{
    public class InvoiceFormModel
    {
        
        public decimal PriceSold { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateSold { get; set; } = DateTime.Now;
        public string InvoiceNumber { get; set; }

        public int VehicleId { get; set; }
        public string FullVehicleName { get; set; }

    }
}
