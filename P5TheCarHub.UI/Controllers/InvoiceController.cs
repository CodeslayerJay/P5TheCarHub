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
using P5TheCarHub.UI.Models.Validators;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;

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

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int vehicleId, InvoiceFormModel formModel)
        {
            try
            {
                var validator = new InvoiceValidator();
                var results = validator.Validate(formModel);

                if (results.Errors.Any())
                {
                    foreach (var error in results.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (!ModelState.IsValid)
                    return View("InvoiceForm", formModel);

                var invoice = (formModel.InvoiceId == AppStrings.NotSet) ?
                        _mapper.Map<Invoice>(formModel) :
                        _mapper.Map<InvoiceFormModel, Invoice>(formModel, _invoiceService.GetInvoice(formModel.InvoiceId));

                _invoiceService.SaveInvoice(invoice);

                TempData["SuccessMessage"] = AppStrings.InvoiceSavedSuccessMsg;
                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
            catch (VehicleNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Vehicle");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error occurred while attempting to save repair. {ex.Message}");
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Vehicle");
            }
        }

        //[HttpGet("confim-delete/{id}")]
        //public IActionResult ConfirmDelete(int vehicleId, int id)
        //{
        //    try
        //    {
        //        var repair = _mapper.Map<RepairViewModel>(_repairService.GetById(id));

        //        if (repair == null)
        //        {
        //            TempData["InfoMessage"] = AppStrings.RepairNotFoundMsg;
        //            return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        //        }

        //        return View(repair);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogWarning(ex.Message);
        //        TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
        //        return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        //    }
        //}

        //[HttpPost("delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int VehicleId, int InvoiceId)
        //{
        //    try
        //    {
        //        _invoiceService.De(RepairId);

        //        TempData["InfoMessage"] = AppStrings.RepairDeleteSuccessMsg;


        //    }
        //    catch (RepairNotFoundException ex)
        //    {
        //        TempData["ErrorMsg"] = ex.Message;
        //        _logger.LogWarning(ex.Message);

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
        //        _logger.LogWarning($"Error occured attempting to delete vehicle {ex.Message}");
        //    }

        //    return RedirectToAction("Details", "Vehicle", new { id = VehicleId });
        //}
    }
}