using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P5TheCarHub.UI.Models.ViewModels;
using P5TheCarHub.UI.Utilities;

namespace P5TheCarHub.UI.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private ILogger<AccountController> _logger;

        public AccountController(SignInManager<IdentityUser> signInManger, 
            UserManager<IdentityUser> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManger;
            _userManager = userManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await _signInManager.SignOutAsync();

            return View();
        }

        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(viewModel.Username);

                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Cars");
                        }
                        else
                        {
                            TempData["Message"] = "Incorrect username or password. Please try again.";
                            return View(viewModel);
                        }
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    TempData["Message"] = AppStrings.GenericErrorMsg;
                    return View();
                }
                
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Index", "Home");
            }
            
        
        }
    }
}