using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5CarSalesAppBasic.Helpers;
using P5CarSalesAppBasic.Models.Interfaces.Services;
using P5CarSalesAppBasic.ViewMappers;
using P5CarSalesAppBasic.ViewModels;

namespace P5CarSalesAppBasic.Controllers
{
    [Authorize]
    [Route("manage/invoices")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICarService _carService;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IInvoiceService invoiceService, 
            ICarService carService, ILogger<InvoicesController> logger)
        {
            _invoiceService = invoiceService;
            _carService = carService;
            _logger = logger;
        }

        public IActionResult Index(string search, int page = 1, int size = 5)
        {
            try
            {
                var invoices = _invoiceService.GetAllViewModels();

                if (!String.IsNullOrEmpty(search))
                {
                    invoices = _invoiceService.SearchCustomers(search);
                }

                var viewModel = new InvoiceIndexViewModel
                {
                    Invoices = invoices.Skip((page - 1) * size).Take(size),
                    Pagination = new Pagination(invoices.Count(), size, page)
                };

                return View(viewModel);
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
                    ViewData["InfoMessage"] = "Car not found";
                    return RedirectToAction(nameof(Index));
                }

                if (car.Invoice != null)
                {
                    TempData["InfoMessage"] = "Cannot create another invoice for car.";

                    return RedirectToAction("Details", "Cars", new { id = car.Id });
                }

                var vm = new InvoiceFormModel
                {
                    CarFullName = car.FullCarName,
                    CarId = car.Id
                };

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
        public IActionResult Create(InvoiceFormModel viewModel)
        {
            try
            {
                var car = _carService.GetById(viewModel.CarId);

                if (car == null)
                {
                    ViewData["InfoMessage"] = "Car not found";
                    return RedirectToAction(nameof(Index));
                }

                var errors = _invoiceService.CheckForModelErrors(viewModel, car.Detail.PurchaseDate);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (ModelState.IsValid)
                {
                    _invoiceService.AddInvoice(InvoiceMapper.MapToEntity(viewModel));

                    TempData["SuccessMessage"] = "Invoice added successfully.";
                    return RedirectToAction("Details", "Cars", new { id = car.Id });
                }

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Create), new { id = viewModel.CarId });
            }
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(id);

                if (invoice == null)
                {
                    TempData["InfoMessage"] = "Invoice not found.";
                    return RedirectToAction("Index", "Cars");
                }

                var viewModel = InvoiceMapper.MapToFormModel(invoice);

                TempData["InvoiceId"] = invoice.Id;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InvoiceFormModel viewModel)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(id);

                var errors = _invoiceService.CheckForModelErrors(viewModel, invoice.Car.Detail.PurchaseDate);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (ModelState.IsValid)
                {
                    _invoiceService.UpdateInvoice(InvoiceMapper.MapToUpdateEntity(invoice, viewModel));

                    TempData["SuccessMessage"] = "Updated invoice successfully.";
                    return RedirectToAction("Details", "Cars", new { id = invoice.CarId });
                }

                TempData["InvoiceId"] = invoice.Id;
                return View(viewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(Edit), new { id });
            }
            
        }


        [HttpGet("confirm-delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(id);

                if (invoice == null)
                {
                    TempData["InfoMessage"] = "Invoice not found.";
                    return RedirectToAction("Index", "Cars");
                }

                var vm = InvoiceMapper.MapToInvoiceViewModel(invoice);

                return View(vm);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction("Index", "Cars");
            }
            
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(InvoiceViewModel viewModel)
        {
            try
            {
                var invoice = _invoiceService.GetInvoice(viewModel.Id);

                if (invoice == null)
                {
                    TempData["InfoMessage"] = "Invoice not found";
                    return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });
                }

                TempData["SuccessMessage"] = "Invoice deleted.";
                _invoiceService.DeleteInvoice(invoice.Id);
                return RedirectToAction("Details", "Cars", new { id = viewModel.CarId });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = MagicStrings.GenericErrorMsg;
                return RedirectToAction(nameof(ConfirmDelete), new { id = viewModel.Id });
            }
            
        }
    }
}