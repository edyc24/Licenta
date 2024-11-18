using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}