using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var viewModel = new HomeViewModel
            {
                Vehicles = _vehicleService.GetAll(amount: 3, orderBy: "LotDate").Select(x => _mapper.Map<VehicleViewModel>(x))
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
                    var vehicle = _vehicleService.GetVehicle(vehicleId.Value, withIncludes: true);

                    if (vehicle != null)
                    {
                        contactFormModel.Vehicle = new ContactFormVehicleDetails
                        {
                            Id = vehicle.Id,
                            FullVehicleName = _vehicleService.GetFullVehicleName(vehicle),
                            Photo = (vehicle.Photos.Any()) ? vehicle.Photos.FirstOrDefault(x => x.IsMain).ImageUrl : null,
                            Mileage = vehicle.Mileage?.ToString(),
                            VIN = vehicle.VIN,
                            SalePrice = vehicle.SalePrice.ToString("c")
                        };
                    }
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
