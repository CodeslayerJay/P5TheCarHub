using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class VehicleDetailViewModel
    {
        public int Id { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LotDate { get; set; }

        public decimal SalePrice { get; set; }
    }
}
