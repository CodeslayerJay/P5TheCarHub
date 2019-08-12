using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Models.Entities;
using P5CarSalesAppBasic.Helpers;
using P5CarSalesAppBasic.Models.Interfaces.Services;
using P5CarSalesAppBasic.ViewMappers;
using P5CarSalesAppBasic.ViewModels;

namespace P5CarSalesAppBasic.Controllers
{
    [Authorize]
    [Route("manage/photos")]
    public class PhotosController : Controller
    {
        private ICarService _carService;
        private IPhotoService _photoService;
        private ILogger<PhotosController> _logger;

        public PhotosController(IPhotoService photoService, ICarService carService,
            ILogger<PhotosController> logger)
        {
            _carService = carService;
            _photoService = photoService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult ChangeMainPhoto(int id)
        {
            try
            {
                var photo = _photoService.GetPhoto(id);

                if (photo == null)
                {
                    TempData["InfoMessage"] = "Photo not found.";
                    return RedirectToAction("Index", "Cars");
                }

                _photoService.ChangeMainPhoto(photo.Id);

                TempData["SuccessMessage"] = "Updated main vehicle photo.";
                return RedirectToAction("Details", "Cars", new { id = photo.CarId });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
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

                var vm = new PhotoFormModel
                {
                    Car = CarMapper.MapToCarViewModel(car)
                };

                if (car.Photos.Count == 0)
                {
                    vm.IsMain = true;
                }

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Details", "Cars", new { id });
            }
            
        }

        [HttpPost("create/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, PhotoFormModel viewModel)
        {
            try
            {
                var car = _carService.GetById(viewModel.Car.CarId);

                if (car == null)
                {
                    TempData["InfoMessage"] = "Car not found.";
                    return RedirectToAction("Index", "Cars");
                }

                
                var formFiles = HttpContext.Request.Form.Files;

                if (!_photoService.ValidateFormImages(formFiles, true))
                    ModelState.AddModelError("Photo", "Please select a valid image.");
                
                if (ModelState.IsValid)
                {
                    _photoService.SavePhotos(formFiles, car.Id, viewModel.Description, viewModel.IsMain);
                    
                    TempData["SuccessMessage"] = "Photo uploaded successfully.";
                    return RedirectToAction("Details", "Cars", new { id = car.Id });
                }

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Create), new { id = id });
            }
            
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var photo = _photoService.GetPhoto(id);

                if (photo == null)
                {
                    TempData["InfoMessage"] = "Photo not found.";
                    return RedirectToAction("Index", "Cars");
                }

                if (photo.IsMain)
                {
                    var newMain = _photoService.GetPhotosByCarId(photo.CarId).Where(x => x.IsMain == false).FirstOrDefault();

                    if (newMain != null)
                        _photoService.ChangeMainPhoto(newMain.Id);
                }

                _photoService.DeletePhoto(photo.Id);

                TempData["SuccessMessage"] = "Photo deleted successfully.";
                return RedirectToAction("Details", "Cars", new { id = photo.CarId });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }
    }
}