using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Models.Validators;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.Services;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    //TODO: Add authorization
    [Route("manage/vehicles")]
    public class VehicleController : Controller
    {
        private readonly Adapter _mapper;
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        private readonly VehicleValidationService _vehicleValidator;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
            _mapper = new Adapter();
            _vehicleService = vehicleService;
            _logger = logger;
            _vehicleValidator = new VehicleValidationService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View("VehicleForm", new VehicleFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(VehicleFormModel formModel)
        {
            var inputErrors = _vehicleValidator.CheckForInputErrors(formModel);

            if (inputErrors.Any())
            {
                foreach(var error in inputErrors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehicleService.SaveVehicle(formModel.Adapt<Vehicle>());

                    TempData["SuccessMessage"] = AppStrings.VehicleSavedSuccessMsg;
                    return RedirectToAction(nameof(Add));
                }
                catch(DuplicateVehicleVinException ex)
                {
                    ModelState.AddModelError("VIN", ex.Message);
                    return View("VehicleForm", formModel);
                }
                catch(Exception ex)
                {
                    _logger.LogWarning("Error attempting to save vehicle", ex.Message);
                    TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                    return View("VehicleForm", formModel);
                }
                   
            }

            return View("VehicleForm", formModel);

        }

        
    }
}