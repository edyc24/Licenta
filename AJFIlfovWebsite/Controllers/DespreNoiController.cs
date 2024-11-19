using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class DespreNoiController : Controller
{
    private readonly AnuntService _anuntService;

    public DespreNoiController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    public IActionResult Index()
    {
        return View(); // Transmite lista de anunțuri către view
    }
}