using AJFIlfov.BusinessLogic.Implementation.EchipeService;
using AJFIlfov.BusinessLogic.Implementation.EchipeService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class EchipeController : BaseController
{
    private readonly EchipeService _service;

    public EchipeController(ControllerDependencies dependencies, EchipeService service)
        : base(dependencies)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var echipe = _service.GetAll();
        return View(echipe);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EchipaModel model)
    {
        if (ModelState.IsValid)
        {
            _service.CreateEchipa(model);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var echipa = _service.GetById(id);
        if (echipa == null) return NotFound();

        return View(echipa);
    }

    [HttpPost]
    public IActionResult Edit(EchipaModel model)
    {
        if (ModelState.IsValid)
        {
            _service.UpdateEchipa(model);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var echipa = _service.GetById(id);
        if (echipa == null) return NotFound();

        return View(echipa);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(Guid id)
    {
        _service.DeleteEchipa(id);
        return RedirectToAction("Index");
    }
}