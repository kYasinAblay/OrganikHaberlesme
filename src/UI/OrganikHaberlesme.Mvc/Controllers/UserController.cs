using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Contracts;
using OrganikHaberlesme.Mvc.Models.User;

using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using OrganikHaberlesme.Mvc.BackgroundJobs;

using Microsoft.Extensions.Options;
using OrganikHaberlesme.Mvc.Services.Base;

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

                if (provider == "Email")
                {
                    FireAndForget.EmailSendToUser(verification);
                }  //else if (provider == "SMS")
                   //{
                   //    FireAndForget.EmailSendToUser(new VerificationNotify
                   //    {
                   //        Code = code,
                   //        MailTo = User.Identity.Name,
                   //        Message = "Doğrulama Kodu"
                   //    });
                   //}
                return Json(verification.Code);
            }
            return Json("-1");
        }

        [HttpGet]
        public async Task<IActionResult> ProviderSelection(string? returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProviderSelection(AuthOptions options, string? returnUrl)
        {
            var authResponse = await _authService.AuthenticateOtp(options);
            ViewBag.JWTToken = _localStorageService.GetStorageValue<string>("token");

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
                var returnUrl = Url.Content("~/");
                var isCreated = await _authService.Register(registration);
                if (isCreated)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
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
