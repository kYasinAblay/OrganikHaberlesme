using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Contracts;
using OrganikHaberlesme.Mvc.Models.User;

using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using OrganikHaberlesme.Mvc.BackgroundJobs;

using Microsoft.Extensions.Options;
using OrganikHaberlesme.Mvc.Services.Base;
using OrganikHaberlesme.Mvc.ExternalServices.Model.OrganikAPI;
using OrganikHaberlesme.Mvc.ExternalServices.Enums;
using System;
using OrganikHaberlesme.Mvc.ExternalServices.Extension;
using System.Collections.Generic;

namespace OrganikHaberlesme.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly ILocalStorageService _localStorageService;
        public UserController(IAuthenticationService authService, ILocalStorageService localStorageService)
        {
            _authService = authService;
            _localStorageService = localStorageService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");

                var isLoggedIn = await _authService.Authenticate(login.Email, login.Password);

                //null ise login işlemi devam ediyor demektir. 2FA
                if (isLoggedIn == null)
                {
                    return RedirectToAction(nameof(ProviderSelection));
                }
                if (isLoggedIn == true)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Log In Attempt Failed. Please try again.");
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> GetGenerateCode(string provider)
        {
            if (!string.IsNullOrEmpty(provider))
            {
                VerificationNotify verification = await _authService.GetLogin2FACode(provider);
                switch (provider.ToEnum<Provider>())
                {
                    case Provider.Phone:
                        FireAndForget.SMSSendToUser(verification);
                        break;
                    case Provider.Email:
                    default:
                        FireAndForget.EmailSendToUser(verification);
                        break;
                }

                return Json(verification.Code);
            }
            return Json("-1");
        }

        [HttpGet]
        public IActionResult ProviderSelection(string? returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProviderSelection(AuthOptions options, string? returnUrl)
        {
            var authResponse = await _authService.AuthenticateOtp(options);
            if (authResponse == true)
                ViewBag.JWTToken = _localStorageService.GetStorageValue<string>("token");

            ModelState.AddModelError("","Sending code not compared in selected direction.");
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registration)
        {
            if (ModelState.IsValid)
            {
                //var returnUrl = Url.Content("~/");
                var isCreated = await _authService.Register(registration);
                if (isCreated)
                {
                    ViewBag.Registered = "Kaydedildi!";
                    return View();
                }
            }

            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.\n" + _localStorageService.GetStorageValue<string>("Register"));
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
