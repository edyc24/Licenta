using AJFIlfov.BusinessLogic.Implementation.EchipeService;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Models;
using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class GrupeEchipaController : BaseController
{
    private readonly EchipeService _echipeService;
    private readonly GrupeService _grupeService;
    private readonly GrupeEchipaService _service;

    public GrupeEchipaController(ControllerDependencies dependencies, GrupeEchipaService service,
        EchipeService echipeService, GrupeService grupeService)
        : base(dependencies)
    {
        _service = service;
        _echipeService = echipeService;
        _grupeService = grupeService;
    }

    public IActionResult Index()
    {
        var grupeEchipa = _service.GetAll();
        return View(grupeEchipa);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Echipe = _echipeService.GetAll();
        ViewBag.Grupe = _grupeService.GetAll();
        return View();
    }

    [HttpPost]
    public IActionResult Create(GrupaEchipaCreateModel model)
    {
        if (ModelState.IsValid)
        {
            _service.CreateGrupaEchipa(model);
            return RedirectToAction("Index");
        }

        ViewBag.Echipe = _echipeService.GetAll();
        ViewBag.Grupe = _grupeService.GetAll();
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var grupaEchipa = _service.GetById(id);
        if (grupaEchipa == null) return NotFound();

        ViewBag.Echipe = _echipeService.GetAll();
        ViewBag.Grupe = _grupeService.GetAll();
        return View(grupaEchipa);
    }

    [HttpPost]
    public IActionResult Edit(GrupaEchipaModel model)
    {
        if (ModelState.IsValid)
        {
            _service.UpdateGrupaEchipa(model);
            return RedirectToAction("Index");
        }

        ViewBag.Echipe = _echipeService.GetAll();
        ViewBag.Grupe = _grupeService.GetAll();
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var grupaEchipa = _service.GetById(id);
        if (grupaEchipa == null) return NotFound();

        return View(grupaEchipa);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(Guid id)
    {
        _service.DeleteGrupaEchipa(id);
        return RedirectToAction("Index");
    }
}