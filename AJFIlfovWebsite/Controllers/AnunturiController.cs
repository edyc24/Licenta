// Controllers/AnunturiController.cs

using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class AnunturiController : Controller
{
    private readonly AnuntService _anuntService;

    public AnunturiController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    public IActionResult Index()
    {
        var anunturi = _anuntService.GetAllAnunturi() ?? new List<AnuntModel>();
        ;
        return View(anunturi);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AnuntModel model, IFormFile imagine)
    {
        if (imagine != null && imagine.Length > 0)
            using (var ms = new MemoryStream())
            {
                await imagine.CopyToAsync(ms);
                model.Imagine = ms.ToArray();
            }

        _anuntService.CreateAnunt(model);
        return RedirectToAction("Index");
    }

    public IActionResult Detalii(int id)
    {
        var anunt = _anuntService.GetAnuntById(id);
        if (anunt == null) return NotFound();
        return View(anunt);
    }
}