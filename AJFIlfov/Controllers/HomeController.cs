using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.DataAccess;
using AJFIlfov.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AJFIlfov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MeciuriService _service;

        public HomeController(ILogger<HomeController> logger, MeciuriService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var meciuri = _service.GetAllCurrentUser();

            ViewBag.Meciuri = meciuri.Where(m => m.DataJoc >= DateTime.Now);

            return View();
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