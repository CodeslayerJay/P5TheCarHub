using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Interfaces.Services;
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

        public PhotoController(IPhotoService photoService, IVehicleService vehicleService,
            ILogger<PhotoController> logger, IMapper mapper)
        {
            _photoService = photoService;
            _vehicleService = vehicleService;
            _logger = logger;
            _mapper = mapper;
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

        //[HttpPost("save")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Save(int vehicleId, PhotoFormModel viewModel)
        //{
        //    try
        //    {
        //        var formFiles = HttpContext.Request.Form.Files;

        //        if (!_photoService.ValidateFormImages(formFiles, true))
        //            ModelState.AddModelError("Photo", "Please select a valid image.");

        //        if (ModelState.IsValid)
        //        {
        //            _photoService.SavePhotos(formFiles, car.Id, viewModel.Description, viewModel.IsMain);

        //            TempData["SuccessMessage"] = "Photo uploaded successfully.";
        //            return RedirectToAction("Details", "Cars", new { id = car.Id });
        //        }

        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
        //        return RedirectToAction(nameof(Create), new { id = id });
        //    }

        //}
    }
}