using Microsoft.AspNetCore.Mvc.Rendering;
using P5TheCarHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class VehicleFormModel
    {
        public VehicleFormModel()
        {
            BuildSelectListForYear();
        }

        public int VehicleId { get; set; }
        public string VIN { get; set; }
        public string Mileage { get; set; }
        public string Year { get; set; }
        public List<SelectListItem> YearSelectList { get; set; }

        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public DateTime LotDate { get; set; } = DateTime.Now;
        public decimal SalePrice { get; set; }

        public DateTime? SaleDate { get; set; }
        public VehicleAvailabilityStatus VehicleStatus { get; set; }

        public bool AddRepairOption { get; set; }


        private void BuildSelectListForYear()
        {
            YearSelectList = new List<SelectListItem>();
            var startingYear = 1989;
            var endingYear = DateTime.Now.Year + 1;
            for (var i = endingYear; i > startingYear; i--)
            {
                var selected = (i == endingYear) ? true : false;

                YearSelectList.Add(
                    new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = selected }
                );
            }
        }
    }
}
