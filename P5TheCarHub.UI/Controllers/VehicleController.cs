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
        
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        private readonly VehicleValidationService _vehicleValidator;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
          
            _vehicleService = vehicleService;
            _logger = logger;
            _vehicleValidator = new VehicleValidationService();
        }

        public IActionResult Index()
        {
            var vehicles = _vehicleService.GetAll().Select(v => new VehicleViewModel
            {
                VehicleId = v.Id,
                Year = v.Year.ToString(),
                Make = v.Make,
                Model = v.Model,
                Trim = v.Trim,
                PurchaseDate = v.PurchaseDate,
                PurchasePrice = v.PurchasePrice,
                VIN = v.VIN,
                SalePrice = v.SalePrice,
                IsSold = v.IsSold,
                Color = v.Color,
                FullVehicleName = $"{v.Year} {v.Make} {v.Model} {v.Trim}",
                LotDate = v.LotDate,
                Mileage = v.Mileage
            }).ToList();

            var vm = new VehicleIndexViewModel
            {
                Vehicles = vehicles
            };

            return View(vm);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View("VehicleForm", new VehicleFormModel());
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var vehicle = _vehicleService.GetVehicle(id, withIncludes: false);

            if(vehicle == null)
            {
                ViewData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                return RedirectToAction(nameof(Index));
            }
            
            var vm = vehicle.Adapt<VehicleFormModel>();
            

            return View("VehicleForm", vm);
        }

        [HttpPost("save")]
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
                    var vehicle = (formModel.VehicleId == 0) ? formModel.Adapt<Vehicle>() :
                        formModel.Adapt(_vehicleService.GetVehicle(formModel.VehicleId, withIncludes: false));

                    _vehicleService.SaveVehicle(vehicle);

                    TempData["SuccessMessage"] = AppStrings.VehicleSavedSuccessMsg;
                    return RedirectToAction(nameof(Details), new { id = vehicle.Id });
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

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {

            var vehicle = _vehicleService.GetVehicle(id, withIncludes: true);

            if (vehicle == null)
            {
                ViewData["InfoMessage"] = "Vehicle not found";
                return RedirectToAction(nameof(Index));
            }
           
            //TODO: Figure out mapping id -> vehicleId
            var vm = vehicle.Adapt<VehicleViewModel>();
            vm.VehicleId = vehicle.Id;
            
            return View(vm);
        }
    }
}