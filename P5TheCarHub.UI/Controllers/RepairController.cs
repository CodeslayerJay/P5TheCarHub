using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    //[Authorize]
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

        [HttpGet("add")]
        public IActionResult Add(int vehicleId)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(vehicleId, withIncludes: false);
                if(vehicle == null)
                {
                    ViewData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                    return RedirectToAction("Index", "Vehicle");
                }

                var vm = new RepairFormModel
                {
                    VehicleId = vehicle.Id,
                    VehicleFullName = _vehicleService.GetFullVehicleName(vehicle),
                    ReturnUrl = $"/manage/vehicles/{vehicleId}/repairs/add"
                };

                return View("RepairForm",vm);

            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
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

                var repair = (formModel.RepairId == 0) ?
                        _mapper.Map<Repair>(formModel) :
                        _mapper.Map<RepairFormModel, Repair>(formModel, _repairService.GetById(formModel.RepairId));

                _repairService.SaveRepair(repair);

                ViewData["SuccessMessage"] = AppStrings.RepairSavedSuccessMsg;
                return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
            }
            catch(RepairNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return Redirect(formModel.ReturnUrl);
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Error occurred while attempting to save repair. {ex.Message}");
                ViewData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return Redirect(formModel.ReturnUrl);
            }
        }

        [HttpGet("confim-delete/{id}")]
        public IActionResult ConfirmDelete(int vehicleId, int id)
        {
            try
            {
                var repair = _mapper.Map<RepairViewModel>(_repairService.GetById(id));

                if (repair == null)
                {
                    ViewData["InfoMessage"] = AppStrings.RepairNotFoundMsg;
                    return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                }

                return View(repair);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
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

                ViewData["InfoMessage"] = AppStrings.RepairDeleteSuccessMsg;


            }
            catch (RepairNotFoundException ex)
            {
                ViewData["ErrorMsg"] = ex.Message;
                _logger.LogWarning(ex.Message);

            }
            catch (Exception ex)
            {
                ViewData["ErrorMsg"] = AppStrings.GenericErrorMsg;
                _logger.LogWarning($"Error occured attempting to delete vehicle {ex.Message}");
            }

            return RedirectToAction("Details", "Vehicle", new { id = VehicleId });
        }
    }
}
