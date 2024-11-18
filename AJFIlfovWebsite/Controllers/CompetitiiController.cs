using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class CompetitiiController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}