using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Models.Validators;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    // [Authorize]
    [Route("manage/vehicles")]
    public class VehicleController : Controller
    {
        
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger,
            IMapper mapper)
        {
          
            _vehicleService = vehicleService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var vm = new VehicleIndexViewModel
            {
                Vehicles = _vehicleService.GetAll().Select(v =>
                _mapper.Map<VehicleViewModel>(v))
            };

            return View(vm);
        }

        [HttpGet("Edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            try
            {
                var vm = new VehicleFormModel();

                if (id.HasValue)
                {
                    var vehicle = _vehicleService.GetVehicle(id.Value, withIncludes: false);

                    if (vehicle == null)
                    {
                        ViewData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                        return RedirectToAction(nameof(Index));
                    }

                    vm = _mapper.Map<VehicleFormModel>(vehicle);
                }

                return View("VehicleForm", vm);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(VehicleFormModel formModel)
        {
            var _validator = new VehicleValidator();
            var results = _validator.Validate(formModel);

            if (results.Errors.Any())
            {
                foreach(var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehicle = (formModel.VehicleId == AppStrings.NotSet) ? 
                        _mapper.Map<Vehicle>(formModel) :
                        _mapper.Map<VehicleFormModel, Vehicle>(formModel, _vehicleService.GetVehicle(formModel.VehicleId, withIncludes: false));

                    _vehicleService.SaveVehicle(vehicle);

                    TempData["SuccessMessage"] = AppStrings.VehicleSavedSuccessMsg;
                    return RedirectToAction(nameof(Details), new { id = vehicle.Id });
                }
                catch(DuplicateVehicleVinException ex)
                {
                    ModelState.AddModelError("VIN", ex.Message);
                    return View("VehicleForm", formModel);
                }
                catch(VehicleNotGreaterThanRequiredYearException ex)
                {
                    ModelState.AddModelError("Year", ex.Message);
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
            try
            {
                var vehicle = _vehicleService.GetVehicle(id, withIncludes: true);

                if (vehicle == null)
                {
                    ViewData["InfoMessage"] = "Vehicle not found";
                    return RedirectToAction(nameof(Index));
                }

                var vm = _mapper.Map<VehicleViewModel>(vehicle);

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Index));
            }
            
        }

        [HttpGet("confirm-delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var vehicle = _mapper.Map<VehicleViewModel>(_vehicleService.GetVehicle(id, withIncludes: false));

                if (vehicle == null)
                {
                    ViewData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                    return RedirectToAction(nameof(Index));
                }

                return View(vehicle);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Index));
            }
                

            
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int VehicleId)
        {
            try
            {
                _vehicleService.DeleteVehicle(VehicleId);

                ViewData["InfoMessage"] = AppStrings.VehicleDeletedSuccessMsg;
                

            }
            catch(VehicleNotFoundException ex)
            {
                ViewData["ErrorMsg"] = ex.Message;
                

            }
            catch(Exception ex)
            {
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                _logger.LogWarning($"Error occured attempting to delete vehicle {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}