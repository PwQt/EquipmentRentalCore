using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EquipmentRentalCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Models.AccountViewModels.RegisterViewModel registerViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerViewModel.Login

                };
                if (registerViewModel.Password == registerViewModel.ConfirmPassword)
                {
                    user.Password = registerViewModel.Password;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, registerViewModel.Password);
                    var result = await _userManager.CreateAsync(user);
                    _logger.LogInformation("User has been created");
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction(nameof(HomeController.Index));
                    }
                }
                else
                    ModelState.AddModelError(string.Empty, "Hasła się nie zgadzają!");
            }
            return View(registerViewModel);
        }
    }
}