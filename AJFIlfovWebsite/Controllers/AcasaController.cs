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
        // Obține ultimele 3 anunțuri în ordine descrescătoare după dataPublicare
        var anunturi = _anuntService.GetAllAnunturi()?
            .OrderByDescending(a => a.DataPublicarii)
            .Take(3)
            .ToList();

        return View(anunturi); // Transmite lista de anunțuri către view
    }
}