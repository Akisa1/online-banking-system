
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBankingSystem.Entity;
using OnlineBankingSystem.Models;
using OnlineBankingSystem.Services;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace OnlineBankingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankingService _bankingService;

        public HomeController(ILogger<HomeController> logger, BankingService bankingService)
        {
            _logger = logger;
            _bankingService = bankingService;
        }

        public IActionResult Index()
        {
            var model = _bankingService.GetUserAccount();
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {


            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public int PostRegister([FromBody] RegisterUserRequestDto request)
        {
            return _bankingService.RegisterUser(request);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {

            var user = _bankingService.Login(request);
            if (user == null)
            {
                return View();
            }


            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("FullName", $"{user.Name} {user.Surname}")

                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            return RedirectToAction("Index", "Home");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public bool SendMony([FromBody] SendMonyRequestDto request)
        {
            return _bankingService.SendMony(request);
        }


        [HttpGet]
        public List<UserAccountActivity> GetUserAccountActivity(string iban)
        {
            var model = _bankingService.GetAccountyActivities(iban);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }
    }
}