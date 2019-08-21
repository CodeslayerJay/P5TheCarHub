using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Enums;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces.Services;

using P5TheCarHub.UI.Models;
using P5TheCarHub.UI.Models.ViewModels;

namespace P5TheCarHub.UI.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        
        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService, IMapper mapper)
        {
            
            _logger = logger;
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var filter = new VehicleFilter { Size = 3, IncludePhotos = true };
            
            var viewModel = new HomeViewModel
            {
                VehiclesForSale = _vehicleService.Find(x => x.AvailableStatus == VehicleAvailabilityStatus.Available, filter)
                                    .Select(x => _mapper.Map<VehicleViewModel>(x))
            };

            return View(viewModel);
        }

        public IActionResult Contact(int? vehicleId = null)
        {
            try
            {
                var contactFormModel = new ContactFormModel();

                if (vehicleId.HasValue)
                {
                    contactFormModel.Vehicle = _mapper.Map<VehicleViewModel>(_vehicleService.GetVehicle(vehicleId.Value, withIncludes: true));

                }

                return View(contactFormModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Contact(ContactFormModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                TempData["ContactSuccess"] = true;
                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Contact));
            }

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
