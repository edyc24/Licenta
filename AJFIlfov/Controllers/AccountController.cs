using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.Common.DTOs;
using AJFIlfov.WebApp.Code.Base;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AJFIlfov.Code.Base;
using Microsoft.AspNetCore.Authorization;
using AJFIlfov.BusinessLogic.Implementation.Account.Models;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AccountService Service;

        public AccountController(ControllerDependencies dependencies, AccountService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View("Register", model);
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            Service.RegisterNewUser(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = Service.Login(model.Email, model.Password);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DemoPage()
        {
            var model = Service.GetUsers();

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResetPassword([FromQuery] string email)
        {
            return Json(Service.SendMailResetPassword(email));
        }

        [HttpGet]
        public IActionResult CreateNewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewPassword([FromQuery] string email, [FromQuery] string code)
        {
            return Ok(Service.ValidateCode(email, code));
        }

        [HttpPost]
        public IActionResult ValidatePassword([FromQuery] ValidatePasswordModel model)
        {
            return Json(Service.ValidatePassword(model));
        }

        public IActionResult ProfilePage()
        {
            var currentUserDetails = Service.DisplayProfile();
            return View(currentUserDetails);
        }

        public IActionResult Edit()
        {
            var user = Service.GetUserById();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfilePicture)
        {
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ProfilePicture.CopyToAsync(memoryStream);
                    var userId = Service.GetUserById().Id; 
                    Service.UpdateProfilePicture(userId, memoryStream.ToArray());
                }
            }

            return RedirectToAction("ProfilePage");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModelEdit userModelEdit)
        {
            var user = new CurrentUserDto();

            user = Service.UpdateUser(userModelEdit);
            await LogOut();
            await LogIn(user);
            return RedirectToAction("ProfilePage");
        }

        private async Task LogIn(CurrentUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "AJFIlfovCookies",
                    principal: principal);
        }
        public IActionResult EditAddress(Guid userId)
        {
            var address = Service.GetUserAddress(userId);
            if (address == null)
            {
                address = new UserAddressViewModel
                {
                    UserId = userId,
                    StreetAddress = "test",
                    City = "test",
                    State = "test",
                    ZipCode = "test",
                    Country = "test"
                };
            }
            var model = new UserAddressViewModel
            {
                UserId = address.UserId,
                StreetAddress = address.StreetAddress,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(UserAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var address = $"{model.StreetAddress}, {model.City}, {model.State}, {model.ZipCode}, {model.Country}";
                var coordinates = await Service.GeocodeAddressAsync(address);

                var userAddress = new UserAddress
                {
                    UserId = model.UserId,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Country = model.Country,
                    Latitude = coordinates.Latitude,
                    Longitude = coordinates.Longitude
                };

                Service.SaveUserAddress(userAddress);

                return RedirectToAction("ProfilePage");
            }
            return View(model);
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "AJFIlfovCookies");
        }
    }
}