using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(int invoiceId) : base($"Invoice was not found with id: {invoiceId}")
        { }
    }
}
