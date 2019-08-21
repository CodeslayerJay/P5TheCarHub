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
using P5TheCarHub.Core.Enums;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    [Authorize]
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

        public IActionResult Index(int? VehicleStatus = null, int size = 10, int page = 1)
        {
            try
            {
                var filter = new VehicleFilter { VehicleStatus = VehicleStatus, Size = size, Skip = ((page - 1) * size), IncludePhotos = true };
                var vehicleList = _vehicleService.GetAll(filter).Select(x => _mapper.Map<VehicleViewModel>(x));

                var viewModel = new VehicleIndexViewModel
                {
                    Vehicles = vehicleList,
                    Pagination = new Pagination(vehicleList.Count(), size, page)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Home");
            }

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
                        TempData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                        return RedirectToAction(nameof(Index));
                    }

                    vm = _mapper.Map<VehicleFormModel>(vehicle);
                }

                return View("VehicleForm", vm);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
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

                    if (formModel.AddRepairOption)
                        return RedirectToAction("Edit", "Repair", new { vehicleId = vehicle.Id });

                    TempData["SuccessMessage"] = AppStrings.VehicleSavedSuccessMsg;
                    return RedirectToAction(nameof(Details), new { id = vehicle.Id });
                }
                catch(DuplicateVehicleVinException ex)
                {
                    ModelState.AddModelError("VIN", ex.Message);
                    
                }
                catch(VehicleNotGreaterThanRequiredYearException ex)
                {
                    ModelState.AddModelError("Year", ex.Message);
                    
                }
                catch(Exception ex)
                {
                    _logger.LogWarning("Error attempting to save vehicle", ex.Message);
                    TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                    
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
                    TempData["InfoMessage"] = "Vehicle not found";
                    return RedirectToAction(nameof(Index));
                }

                var vm = _mapper.Map<VehicleViewModel>(vehicle);

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
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
                    TempData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                    return RedirectToAction(nameof(Index));
                }

                return View(vehicle);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
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

                TempData["SuccessMessage"] = AppStrings.VehicleDeletedSuccessMsg;
                
            }
            catch(VehicleNotFoundException ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                

            }
            catch(Exception ex)
            {
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                _logger.LogWarning($"Error occured attempting to delete vehicle {ex.Message}");
                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}