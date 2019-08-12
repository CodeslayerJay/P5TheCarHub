using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Models.Enums;
using P5CarSalesAppBasic.Models.Interfaces.Services;
using P5CarSalesAppBasic.ViewMappers;
using P5CarSalesAppBasic.ViewModels;

namespace P5CarSalesAppBasic.Controllers
{
    public class InventoryController : Controller
    {
        private ICarService _carService;
        private ILogger<InventoryController> _logger;

        public InventoryController(ICarService carService, ILogger<InventoryController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        public IActionResult Index(int? type, string minPrice = null, 
            string maxPrice = "100000", int size = 10, int page = 1)
        {
            try
            {
                var carList = _carService.GetAllViewModels();
                var filterApplied = false;

                if (type != null)
                {
                    carList = _carService.GetAllViewModels().Where(x => x.Type == ((CarType)type));
                    filterApplied = true;

                }

                
                var minPriceParseResult = decimal.TryParse(minPrice, out decimal min);
                var maxPriceParseResult = decimal.TryParse(maxPrice, out decimal max);

                if (minPriceParseResult && maxPriceParseResult)
                {
                    carList = carList.Where(x => x.IsSold == false)
                       .Where(x => x.SalePrice >= min)
                       .Where(x => x.SalePrice < max)
                       .OrderBy(x => x.SalePrice)
                       .OrderBy(x => x.LotDate);
                    filterApplied = true;
                }                   
                else
                {
                    carList = carList.OrderBy(x => x.LotDate).OrderBy(x => x.IsSold);
                }
                    
                var viewModel = new InventoryViewModel
                {
                    Cars = carList.Skip((page - 1) * size).Take(size),
                    IsFilterApplied = filterApplied,
                    Pagination = new Pagination(carList.Count(), size, page)
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
                var car = _carService.GetById(id);

                if (car == null)
                    return RedirectToAction(nameof(Index));

                var viewModel = new InventoryDetailViewModel
                {
                    Car = CarMapper.MapToCarViewModel(car)
                };

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}