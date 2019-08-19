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
using P5TheCarHub.UI.ServiceWorkers;

namespace P5TheCarHub.UI.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeControllerWorker _worker;

        public HomeController(ILogger<HomeController> logger, IHomeControllerWorker worker)
        {
            
            _logger = logger;

            _worker = worker;
            
        }

        public IActionResult Index()
        {
            return View(_worker.ExecuteIndex());
        }

        public IActionResult Contact(int? vehicleId = null)
        {
            try
            {
                return View(_worker.ExecuteContact(vehicleId));
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
