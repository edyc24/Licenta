using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class ComisiiController : Controller
{
    private readonly AnuntService _anuntService;

    public ComisiiController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    public IActionResult Index()
    {
        var anunturiComisii = _anuntService.GetAnunturiComisii();
        return View(anunturiComisii);
    }
}