using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.Controllers;
using AJFIlfov.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AJFIlfovWebsite.Controllers
{
    public class ArbitriiController : Controller
    {
        private readonly ILogger<ArbitriiController> _logger;

        private readonly MeciuriService _service;

        public ArbitriiController(ILogger<ArbitriiController> logger, MeciuriService service)
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
    }
}
