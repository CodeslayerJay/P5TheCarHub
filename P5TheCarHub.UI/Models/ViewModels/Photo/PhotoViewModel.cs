using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5CarSalesAppBasic.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }

    }
}
