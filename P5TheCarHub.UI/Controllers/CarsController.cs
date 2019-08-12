using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Models.Entities;
using P5CarSalesAppBasic.Helpers;
using P5CarSalesAppBasic.Models.Interfaces.Services;
using P5CarSalesAppBasic.ViewMappers;

namespace P5TheCarHub.UI.Controllers
{
    [Authorize]
    [Route("manage/cars")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IDetailService _detailService;
        private readonly IPhotoService _photoService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, IDetailService detailService, 
            IPhotoService photoService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _detailService = detailService;
            _photoService = photoService;
            _logger = logger;
        }


        public IActionResult Index(string search, int page = 1, int size = 5)
        {
            try
            {
                var cars = _carService.GetAllViewModels();

                if (!String.IsNullOrEmpty(search))
                {
                    cars = _carService.SearchInventory(search);
                    TempData["InfoMessage"] = $"Found {cars.Count()} results.";
                }

                var viewModel = new CarIndexViewModel
                {
                    Cars = cars.Skip((page - 1) * size).Take(size),
                    Pagination = new Pagination(cars.Count(), size, page)
                    
                };
                                
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new CarFormModel();
            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarFormModel viewModel)
        {
            try
            {
                var errors = _carService.CheckForModelErrors(viewModel, true);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                var formFiles = HttpContext.Request.Form.Files;

                if (!_photoService.ValidateFormImages(formFiles, false))
                {
                    ModelState.AddModelError("Photo", "Please select a valid image.");
                    TempData["ErrorMessage"] = "Cannot upload image. Not a valid image type.";
                }
                    

                if (ModelState.IsValid)
                {
                    var car = _carService.AddCar(CarMapper.MapToEntity(viewModel));
                    var detail = new Detail
                    {
                        CarId = car.Id,
                        PurchaseDate = viewModel.PurchaseDate,
                        LotDate = viewModel.LotDate,
                        PurchasePrice = viewModel.PurchasePrice
                    };

                    _detailService.AddDetail(detail);

                    _photoService.SavePhotos(formFiles, car.Id, viewModel.PhotoDescription, true);
                    
                    TempData["SuccessMessage"] = "Add vehicle successfully.";

                    if (viewModel.AddRepairOption)
                    {
                        return RedirectToAction("Create", "Repairs", new { id = car.Id });
                    }

                    return RedirectToAction(nameof(Details), new { id = car.Id });
                }

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Create));
            }
           
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                var car = _carService.GetById(id);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["CarId"] = car.Id;
                return View(CarMapper.MapToCarFormModel(car));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = "There was an error. Please try again.";
                return RedirectToAction("Details", "Cars", new { id = id });
            }
            

        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CarFormModel viewModel)
        {
            try
            {
                var car = _carService.GetById(id);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found.";
                    return RedirectToAction(nameof(Index));
                }

                var checkVIN = (car.VIN != viewModel.VIN.ToUpper()) ? true : false;
                var errors = _carService.CheckForModelErrors(viewModel, checkVIN);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (ModelState.IsValid)
                {

                    var updatedCar = _carService.UpdateCar(CarMapper.MapToUpdateEntity(car, viewModel));
                    var detailToUpdate = _detailService.GetDetailByCarId(updatedCar.Id);

                    detailToUpdate.LotDate = viewModel.LotDate;
                    detailToUpdate.PurchaseDate = viewModel.PurchaseDate;
                    detailToUpdate.PurchasePrice = viewModel.PurchasePrice;
                    _detailService.UpdateDetail(detailToUpdate);

                    TempData["SuccessMessage"] = "Updated car successfully.";
                    return RedirectToAction(nameof(Details), new { id = car.Id });
                }

                TempData["CarId"] = car.Id;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            
        }
        
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var model = CarMapper.MapToCarViewModel(_carService.GetById(id));

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpGet("confirm-delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var car = _carService.GetById(id);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found.";
                    return RedirectToAction("Index", "Cars");
                }

                var vm = CarMapper.MapToCarViewModel(car);

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Details", "Cars", new { id = id });
            }
            
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CarViewModel viewModel)
        {
            try
            {
                var car = _carService.GetById(viewModel.CarId);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found";
                    return RedirectToAction("Index", "Cars");
                }

                TempData["SuccessMessage"] = "Car deleted successfully.";
                _carService.DeleteCar(car.Id);
                return RedirectToAction("Index", "Cars");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(ConfirmDelete), new { id = viewModel.CarId });
            }
            
        }
    }
}