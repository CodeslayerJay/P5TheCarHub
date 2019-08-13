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
using P5TheCarHub.Core.Interfaces.Services;
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

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
            _mapper = new Adapter();
            _vehicleService = vehicleService;
            _logger = logger;
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
            var inputErrors = CheckForInputErrors(formModel);

            if (inputErrors.Any())
            {
                foreach(var error in inputErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formModel.VehicleId == AppStrings.NotSet)
                    {
                        _vehicleService.SaveVehicle(formModel.Adapt<Vehicle>());
                        TempData["SuccessMessage"] = AppStrings.VehicleAddSuccessMsg;
                        return RedirectToAction(nameof(Add));
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogWarning("Error attempting to save vehicle", ex.Message);
                    TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                    return RedirectToAction(nameof(Add));
                }
                   
            }

            return View("VehicleForm", formModel);

        }

        private List<ValidationFailure> CheckForInputErrors(VehicleFormModel formModel)
        {
            var errors = new List<ValidationFailure>();
            var validator = new VehicleValidator();

            var result = validator.Validate(formModel);

            return (result.Errors.Any()) ? result.Errors.ToList() : errors;
        }
    }
}