using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class DisponibilitateController : BaseController
{
    private readonly DisponibilitateService Service;

    public DisponibilitateController(ControllerDependencies dependencies, DisponibilitateService service)
        : base(dependencies)
    {
        Service = service;
    }

    public IActionResult Index()
    {
        var cereri = Service.GetCereri();
        return View(cereri);
    }

    public IActionResult AdaugaDisponibilitate(CerereModel model)
    {
        var response = Service.Adauga(model);
        return RedirectToAction("Index", "Disponibilitate");
    }
}