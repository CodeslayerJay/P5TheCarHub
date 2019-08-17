using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Managers;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.Managers;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    [Authorize]
    [Route("manage/vehicles/{vehicleId}/photos")]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<PhotoController> _logger;
        private readonly IMapper _mapper;
        private readonly IPhotoManager<IFormFile> _photoManager;

        public PhotoController(IPhotoService photoService, IVehicleService vehicleService,
            ILogger<PhotoController> logger, IMapper mapper, IPhotoManager<IFormFile> photoManager)
        {
            _photoService = photoService;
            _vehicleService = vehicleService;
            _logger = logger;
            _mapper = mapper;
            _photoManager = photoManager;
        }


        public IActionResult Index(int vehicleId)
        {
            return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        }

        [HttpGet("add")]
        public IActionResult AddPhoto(int vehicleId)
        {
            var vehicle = _vehicleService.GetVehicle(vehicleId, withIncludes: false);

            if(vehicle == null)
            {
                TempData["InfoMessage"] = AppStrings.VehicleNotFoundMsg;
                return RedirectToAction("Index", "Vehicle");
            }

            var vm = new PhotoFormModel
            {
                VehicleId = vehicleId,
                FullVehicleName = _vehicleService.GetFullVehicleName(vehicle),
                IsMain = ((_photoService.GetVehicleMainPhoto(vehicle.Id) == null) ? true : false),
            };

            return View("PhotoForm",vm);
        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int vehicleId, PhotoFormModel formModel)
        {
            try
            {
                if (!_photoManager.ValidateImage(formModel.Photo))
                    ModelState.AddModelError("Photo", "Please select a valid image.");

                if (ModelState.IsValid)
                {
                    var result = _photoManager.UploadImage(formModel.Photo, vehicleId);

                    if (!result.Success)
                        TempData["ErrorMsg"] = AppStrings.PhotoUploadErrorMsg;

                    var photo = _mapper.Map<Photo>(formModel);
                    
                    photo.ImageUrl = result.ImageUrl;

                    _photoService.SavePhoto(photo);

                    TempData["SuccessMessage"] = AppStrings.PhotoSavedSuccessMsg;
                    return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                }

                return View("PhotoForm", formModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction(nameof(AddPhoto), new { id = vehicleId });
            }

        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int vehicleId, int id)
        {
            try
            {
                var photo = _photoService.GetPhoto(id);

                if (photo == null)
                {
                    TempData["InfoMessage"] = AppStrings.PhotoNotFoundMsg;
                    return RedirectToAction("Details", "Vehicle", new { vehicleId });
                }

                var result = _photoManager.DeleteImageFromDisk(photo.ImageUrl);

                if (result.Success)
                    _photoService.DeletePhoto(id);

                TempData["SuccessMessage"] = AppStrings.PhotoDeleteSuccessMsg;
                
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMessage"] = AppStrings.PhotoDeleteErrorMsg;
                
            }

            return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        }

        [HttpPost("update-mainphoto")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMainPhoto(int vehicleId, int photoId)
        {
            try
            {
                var photo = _photoService.GetPhoto(photoId);

                if (photo == null)
                {
                    TempData["InfoMessage"] = AppStrings.PhotoNotFoundMsg;
                    return RedirectToAction("Details", "Vehicle", new { vehicleId });
                }

                var oldMain = _photoService.GetVehicleMainPhoto(vehicleId);
                _photoService.UpdateVehicleMainPhoto(oldMain.Id, photo.Id);

                TempData["SuccessMessage"] = AppStrings.PhotoUpdateMainSuccessMsg;

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;

            }

            return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
        }
    }
}