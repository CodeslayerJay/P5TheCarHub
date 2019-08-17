using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.Validators;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    [Authorize]
    [Route("manage/vehicles/{vehicleId}/repairs")]
    public class RepairController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RepairController(IVehicleService vehicleService, IRepairService repairService, ILogger<RepairController> logger, 
            IMapper mapper)
        {
            _repairService = repairService;
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index(int vehicleId)
        {
            return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        }
        
        [HttpGet("edit/{id?}")]
        public IActionResult Edit(int vehicleId, int? id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(vehicleId, withIncludes: false);

                if(vehicle == null)
                {
                    TempData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                    return RedirectToAction("Index", "Vehicle");
                }

                var vm = new RepairFormModel
                {
                    VehicleId = vehicle.Id
                };

                if (id.HasValue)
                {
                    var repair = _repairService.GetById(id.Value);

                    if (repair == null)
                    {
                        TempData["InfoMessage"] = AppStrings.RepairNotFoundMsg;
                        return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                    }


                    vm = _mapper.Map<RepairFormModel>(repair);
                }

                vm.VehicleFullName = _vehicleService.GetFullVehicleName(vehicle);
                return View("RepairForm", vm);

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Vehicle");
            }

        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int vehicleId, RepairFormModel formModel)
        {
            try
            {
                var validator = new RepairValidator();
                var results = validator.Validate(formModel);

                if (results.Errors.Any())
                {
                    foreach(var error in results.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (!ModelState.IsValid)
                    return View("RepairForm", formModel);

                var repair = (formModel.RepairId == AppStrings.NotSet) ?
                        _mapper.Map<Repair>(formModel) :
                        _mapper.Map<RepairFormModel, Repair>(formModel, _repairService.GetById(formModel.RepairId));

                _repairService.SaveRepair(repair);

                TempData["SuccessMessage"] = AppStrings.RepairSavedSuccessMsg;

                if (formModel.AddAnotherRepair)
                    return RedirectToAction(nameof(Edit), new { vehicleId });

                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
            catch(RepairNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Error occurred while attempting to save repair. {ex.Message}");
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Vehicle");
            }
        }

        [HttpGet("confirm-delete/{id}")]
        public IActionResult ConfirmDelete(int vehicleId, int id)
        {
            try
            {
                var repair = _mapper.Map<RepairViewModel>(_repairService.GetById(id));

                if (repair == null)
                {
                    TempData["InfoMessage"] = AppStrings.RepairNotFoundMsg;
                    return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                }

                return View(repair);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int VehicleId, int RepairId)
        {
            try
            {
                _repairService.DeleteRepair(RepairId);

                TempData["SuccessMessage"] = AppStrings.RepairDeleteSuccessMsg;


            }
            catch (RepairNotFoundException ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                _logger.LogWarning(ex.Message);

            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                _logger.LogWarning($"Error occured attempting to delete vehicle {ex.Message}");
            }

            return RedirectToAction("Details", "Vehicle", new { id = VehicleId });
        }
    }
}
