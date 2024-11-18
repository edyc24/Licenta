using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class HomeWebsiteController : Controller
{
    private readonly AnuntService _anuntService;

    public HomeWebsiteController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    public IActionResult Index()
    {
        // Obține ultimele 3 anunțuri
        var anunturi = _anuntService.GetAllAnunturi()?.Take(3).ToList();
        return View(anunturi); // Transmite lista de anunțuri către view
    }
}