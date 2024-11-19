using AJFIlfov.BusinessLogic.Implementation.Account.Models;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.Common.DTOs;
using AJFIlfov.Entities.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using AJFIlfov.BusinessLogic.Implementation.Audituri;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using Microsoft.AspNetCore.Authorization;

namespace AJFIlfovWebsite.Controllers
{
    [Authorize(Roles = "Administrator")] // Requires Administrator role for all actions by default
    public class AdminController : BaseController
    {
        private readonly AccountService _accountService;
        private readonly AnuntService _anuntService;
        private readonly AuditService _auditService;

        public AdminController(
            ControllerDependencies dependencies,
            AccountService accountService,
            AnuntService anuntService,
            AuditService auditService)
            : base(dependencies)
        {
            _accountService = accountService;
            _anuntService = anuntService;
            _auditService = auditService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Allow anonymous access to Login actions
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = _accountService.Login(model.Email, model.Password);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("Index");
        }

        // Allow anonymous access to Register actions
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            return View("Register", model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            _accountService.RegisterNewUser(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();
            return RedirectToAction("Index");
        }

        private async Task LogIn(CurrentUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role), // Ensure Role is set correctly
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                scheme: "AJFIlfovCookies",
                principal: principal);
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "AJFIlfovCookies");
        }

        // Actions that require Administrator role
        public IActionResult CreateAnunt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnunt(AnuntModel model, IFormFile Imagine)
        {
            if (Imagine != null && Imagine.Length > 0)
            {
                // Validate file type
                if (Imagine.ContentType != "application/pdf")
                {
                    ModelState.AddModelError("Imagine", "Only PDF files are allowed.");
                    return View(model);
                }

                // Limit file size (e.g., max 10 MB)
                if (Imagine.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("Imagine", "File size must be less than 10 MB.");
                    return View(model);
                }

                // Read the PDF file into a byte array
                using (var ms = new MemoryStream())
                {
                    await Imagine.CopyToAsync(ms);
                    model.Imagine = ms.ToArray();
                }
            }

            _anuntService.CreateAnunt(model);

           
            return RedirectToAction("Index", "Anunturi");
        }

        public IActionResult Audit()
        {
            var logs = _auditService.GetAllLogs();
            return View(logs);
        }

    }
}
