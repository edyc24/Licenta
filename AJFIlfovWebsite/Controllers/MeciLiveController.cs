using AJFIlfov.BusinessLogic.Implementation.MeciLiveService;
using AJFIlfov.BusinessLogic.Implementation.MeciLiveService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;

public class MeciLiveController : BaseController
{
    private readonly MeciLiveService _service;

    public MeciLiveController(ControllerDependencies dependencies, MeciLiveService service)
        : base(dependencies)
    {
        _service = service;
    }

    

    public IActionResult Index()
    {
        var meciuriLive = _service.GetAllMeciuriLive();

        // Actualizăm minutul pentru fiecare meci
        foreach (var meciModel in meciuriLive)
        {
            var meciEntity = _service.GetMeciLiveEntityById(meciModel.IdMeciLive);
            _service.UpdateMinut(meciEntity);

            // Actualizăm modelul cu noile valori
            meciModel.Minut = meciEntity.Minut;
            meciModel.Status = meciEntity.Status;
        }

        return View(meciuriLive);
    }

    public IActionResult GetUpdatedMatches()
    {
        var meciuriLive = _service.GetAllMeciuriLive();

        // Actualizăm minutul pentru fiecare meci
        foreach (var meciModel in meciuriLive)
        {
            var meciEntity = _service.GetMeciLiveEntityById(meciModel.IdMeciLive);
            _service.UpdateMinut(meciEntity);

            // Actualizăm modelul cu noile valori
            meciModel.Minut = meciEntity.Minut;
            meciModel.Status = meciEntity.Status;
        }

        // Returnați datele în format JSON
        var meciuriDto = meciuriLive.Select(m => new
        {
            m.IdMeciLive,
            Meci = $"{m.EchipaGazda} vs {m.EchipaOaspete}",
            Scor = $"{m.ScorGazda} - {m.ScorOaspete}",
            Minut = m.Status == "Pauza" ? "Pauză" :
                m.Status == "Finalizat" ? "Final" :
                m.Status == "InDesfasurare" ? $"{m.Minut ?? 0}'" : "-"
        }).ToList();

        return Json(meciuriDto);
    }
}