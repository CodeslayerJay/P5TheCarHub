using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Helpers;
using P5CarSalesAppBasic.Models.Interfaces.Services;
using P5CarSalesAppBasic.ViewMappers;
using P5CarSalesAppBasic.ViewModels;

namespace P5CarSalesAppBasic.Controllers
{
    [Authorize]
    [Route("manage/repairs")]
    public class RepairsController : Controller
    {
        private IRepairService _repairService;
        private ICarService _carService;
        private readonly IDetailService _detailService;
        private readonly ILogger<RepairsController> _logger;

        public RepairsController(IRepairService repairService, ICarService carService, 
            IDetailService detailService, ILogger<RepairsController> logger)
        {
            _repairService = repairService;
            _carService = carService;
            _detailService = detailService;
            _logger = logger;
        }

        [HttpGet("create/{id}")]
        public IActionResult Create(int id)
        {
            try
            {
                var car = _carService.GetById(id);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found.";
                    return RedirectToAction("Index", "Cars");
                }

                var vm = new RepairFormModel
                {
                    CarFullName = car.FullCarName,
                    CarId = car.Id
                };

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpPost("create/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepairFormModel viewModel)
        {
            try
            {
                var car = _carService.GetById(viewModel.CarId);

                if (car == null)
                {
                    ViewData["InfoMessage"] = "Car not found";
                    return RedirectToAction("Index", "Cars");
                }

                var errors = _repairService.CheckForModelErrors(viewModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (ModelState.IsValid)
                {
                    _repairService.AddRepair(RepairMapper.MapToEntity(viewModel));

                    _detailService.UpdateSalePrice(car.Id, _repairService.GetTotalRepairCosts(car.Id));

                    TempData["SuccessMessage"] = "Repair added successfully.";

                    if (viewModel.AddRepair)
                    {
                        return RedirectToAction(nameof(Create), new { id = viewModel.CarId });
                    }

                    return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });
                }

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                var repair = _repairService.GetRepair(id);

                if (repair == null)
                {
                    TempData["InfoMessage"] = "Repair not found.";
                    return RedirectToAction("Index", "Cars");
                }


                TempData["RepairId"] = repair.Id;
                return View(RepairMapper.MapToFormModel(repair));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RepairFormModel viewModel)
        {
            try
            {
                var repair = _repairService.GetRepair(id);

                if (repair == null)
                {
                    TempData["InfoMessage"] = "Repair not found.";
                    return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });
                }

                var errors = _repairService.CheckForModelErrors(viewModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (ModelState.IsValid)
                {
                    _repairService.UpdateRepair(RepairMapper.MapToUpdateEntity(repair, viewModel));

                    TempData["SuccessMessage"] = "Repair updated successfully.";

                    if (viewModel.AddRepair)
                    {
                        return RedirectToAction(nameof(Create), new { id = viewModel.CarId });
                    }

                    return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });
                }

                TempData["RepairId"] = repair.Id;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            
        }


        [HttpGet("confirm-delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var repair = _repairService.GetRepair(id);

                if (repair == null)
                {
                    TempData["InfoMessage"] = "Repair not found.";
                    return RedirectToAction("Index", "Cars");
                }

                var vm = RepairMapper.MapToViewModel(repair);

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(RepairViewModel viewModel)
        {
            try
            {
                var repair = _repairService.GetRepair(viewModel.Id);

                if (repair == null)
                {
                    TempData["InfoMessage"] = "Repair not found";
                    return RedirectToAction("Index", "Cars");
                }

                TempData["SuccessMessage"] = "Repair Deleted.";
                _repairService.DeleteRepair(repair.Id);
                return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(ConfirmDelete), new { id = viewModel.Id });
            }
            
        }
    }
}