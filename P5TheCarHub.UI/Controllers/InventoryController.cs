using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.Core.Filters;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.ViewModels;

namespace P5TheCarHub.UI.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ILogger<InventoryController> _logger;
        private readonly IMapper _mapper;

        public InventoryController(IVehicleService vehicleService, ILogger<InventoryController> logger,
            IMapper mapper)
        {
            _vehicleService = vehicleService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index(decimal? minPrice = null,
                decimal? maxPrice = 50000, int size = 10, int page = 1)
        {
            try
            {
                var filter = new VehicleFilter { MinPrice = minPrice, Size = size, MaxPrice = maxPrice, Skip = ((page - 1) * size),
                        IncludePhotos = true};
                var vehicles = _vehicleService.GetAll(filter).Select(x => _mapper.Map<VehicleViewModel>(x));
                
                var viewModel = new InventoryViewModel
                {
                    Vehicles = vehicles,
                    Pagination = new Pagination(vehicles.Count(), size, page)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(id, withIncludes: true);

                if (vehicle == null)
                    return RedirectToAction(nameof(Index));

                var viewModel = new InventoryDetailViewModel
                {
                    Vehicle = _mapper.Map<VehicleViewModel>(vehicle)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}