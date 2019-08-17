using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    // [Authorize]
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
                FullVehicleName = _vehicleService.GetFullVehicleName(vehicle)
            };

            return View("PhotoForm",vm);
        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(int vehicleId, PhotoFormModel formModel)
        {
            try
            {
                var image = HttpContext.Request.Form.Files[0];

                if (!_photoManager.ValidateImage(image))
                    ModelState.AddModelError("Photo", "Please select a valid image.");

                if (ModelState.IsValid)
                {
                    var result = _photoManager.UploadImage(image, vehicleId);

                    if (!result.Success)
                        TempData["ErrorMsg"] = AppStrings.ErrorUploadingImgMsg;

                    var photo = _mapper.Map<Photo>(formModel);
                    photo.ImageUrl = result.ImagePath;

                    _photoService.SavePhoto(photo);

                    TempData["SuccessMessage"] = "Photo uploaded successfully.";
                    return RedirectToAction("Details", "Vehicle", new { id = vehicleId });
                }

                return View("PhotoForm", formModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = AppStrings.GenericErrorMsg;
                return RedirectToAction(nameof(AddPhoto), new { vehicleId });
            }

        }
    }
}