using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index(int? type, string minPrice = null,
                string maxPrice = "100000", int size = 10, int page = 1)
        {
            try
            {
                var vehicleList = _vehicleService.GetAll().Select(x => _mapper.Map<VehicleViewModel>(x));
                var filterApplied = false;

                var minPriceParseResult = decimal.TryParse(minPrice, out decimal min);
                var maxPriceParseResult = decimal.TryParse(maxPrice, out decimal max);

                if (minPriceParseResult && maxPriceParseResult)
                {
                    vehicleList = vehicleList.Where(x => x.IsSold == false)
                       .Where(x => x.SalePrice >= min)
                       .Where(x => x.SalePrice < max)
                       .OrderBy(x => x.SalePrice)
                       .OrderBy(x => x.LotDate);
                    filterApplied = true;
                }
                else
                {
                    vehicleList = vehicleList.OrderBy(x => x.LotDate).OrderBy(x => x.IsSold);
                }

                var viewModel = new InventoryViewModel
                {
                    Vehicles = vehicleList.Skip((page - 1) * size).Take(size),
                    IsFilterApplied = filterApplied,
                    Pagination = new Pagination(vehicleList.Count(), size, page)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Home");
            }

        }

        //[HttpGet("details/{id}")]
        //public IActionResult Details(int id)
        //{
        //    try
        //    {
        //        var car = _vehicleService.GetById(id);

        //        if (car == null)
        //            return RedirectToAction(nameof(Index));

        //        var viewModel = new InventoryDetailViewModel
        //        {
        //            Car = CarMapper.MapToCarViewModel(car)
        //        };

        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return RedirectToAction(nameof(Index));
        //    }
        //}
    }
}