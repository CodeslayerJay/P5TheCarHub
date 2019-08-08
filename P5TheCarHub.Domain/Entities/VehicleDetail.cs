using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Domain.Entities
{
    public class VehicleDetail : BaseEntity
    {
        
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LotDate { get; set; }
        public decimal SalePrice { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
