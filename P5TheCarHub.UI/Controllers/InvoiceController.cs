using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Utilities;
using P5TheCarHub.UI.Models.ViewModels;

namespace P5TheCarHub.UI.Controllers
{
    // [Authroize]
    [Route("manage/vehicles/{vehicleId}/invoice")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService invoiceService, IVehicleService vehicleService, ILogger<InvoiceController> logger, 
            IMapper mapper)
        {
            _invoiceService = invoiceService;
            _logger = logger;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet("edit/{id?}")]
        public IActionResult Edit(int vehicleId, int? id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(vehicleId, withIncludes: false);

                if (vehicle == null)
                {
                    TempData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                    return RedirectToAction("Index", "Vehicle");
                }

                var vm = new InvoiceFormModel
                {
                    VehicleId = vehicle.Id
                };

                if (id.HasValue)
                {
                    var invoice = _invoiceService.GetInvoice(id.Value);

                    if (invoice == null)
                    {
                        TempData["InfoMessage"] = AppStrings.InvoiceNotFoundMsg;
                        return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                    }


                    vm = _mapper.Map<InvoiceFormModel>(invoice);
                }

                vm.VehicleFullName = _vehicleService.GetFullVehicleName(vehicle);
                return View("InvoiceForm", vm);

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Invoices");
            }
        }


    }
}