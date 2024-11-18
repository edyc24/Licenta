// Controllers/DocumenteController.cs

using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class DocumenteController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CerereInscriere()
    {
        return View();
    }

    public IActionResult CerereAfiliere()
    {
        return View();
    }

    public IActionResult TaxeCotizatii()
    {
        return View();
    }
}