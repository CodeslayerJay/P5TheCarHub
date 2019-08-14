
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.ViewModels
{
    public class InvoiceIndexViewModel
    {
        public InvoiceIndexViewModel()
        {
            Invoices = new List<InvoiceViewModel>();
        }
        public IEnumerable<InvoiceViewModel> Invoices { get; set; }
        //public Pagination Pagination { get; set; }
    }
}
