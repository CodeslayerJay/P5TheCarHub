using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace P5TheCarHub.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {
            Repairs = new Collection<Repair>();
            Photos = new Collection<Photo>();
        }
        
        
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LotDate { get; set; }
        public decimal SalePrice { get; set; }

        public bool IsSold { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public Invoice Invoice { get; set; }
        public ICollection<Repair> Repairs { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
