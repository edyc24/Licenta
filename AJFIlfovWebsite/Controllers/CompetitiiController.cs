using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers;


public class CompetitiiController : BaseController
{
    private readonly MeciuriService _service;
    private readonly GrupeService _grupeService;

    public CompetitiiController(ControllerDependencies dependencies, MeciuriService service, GrupeService grupeService) : base(dependencies)
    {
        _service = service;
        _grupeService = grupeService;
    }

    public IActionResult Index()
    {
        ViewBag.Grupe = _grupeService.GetAll();
        return View();
    }

    [HttpGet]
    public IActionResult GetClasament(Guid grupaId)
    {
        var meciuri = _service.GetMeciuriByGrupa(grupaId);
        if (meciuri == null) return NotFound();

        var clasament = meciuri.Where(m => m.ScorGazde != null && m.ScorOaspeti != null)
            .SelectMany(m => new[]
            {
                new
                {
                    Echipa = m.EchipaGazdaNume,
                    M = 1,
                    V = m.ScorGazde > m.ScorOaspeti ? 1 : 0,
                    E = m.ScorGazde == m.ScorOaspeti ? 1 : 0,
                    I = m.ScorGazde < m.ScorOaspeti ? 1 : 0,
                    GM = m.ScorGazde ?? 0,
                    GP = m.ScorOaspeti ?? 0,
                    P = (m.ScorGazde > m.ScorOaspeti ? 3 : 0) + (m.ScorGazde == m.ScorOaspeti ? 1 : 0)
                },
                new
                {
                    Echipa = m.EchipaOaspeteNume,
                    M = 1,
                    V = m.ScorOaspeti > m.ScorGazde ? 1 : 0,
                    E = m.ScorGazde == m.ScorOaspeti ? 1 : 0,
                    I = m.ScorOaspeti < m.ScorGazde ? 1 : 0,
                    GM = m.ScorOaspeti ?? 0,
                    GP = m.ScorGazde ?? 0,
                    P = (m.ScorOaspeti > m.ScorGazde ? 3 : 0) + (m.ScorGazde == m.ScorOaspeti ? 1 : 0)
                }
            })
            .GroupBy(e => e.Echipa)
            .Select(g => new
            {
                Echipa = g.Key,
                M = g.Sum(x => x.M),
                V = g.Sum(x => x.V),
                E = g.Sum(x => x.E),
                I = g.Sum(x => x.I),
                GM = g.Sum(x => x.GM),
                GP = g.Sum(x => x.GP),
                P = g.Sum(x => x.P)
            })
            .OrderByDescending(c => c.P)               // Sort by points
            .ThenByDescending(c => c.GM - c.GP)       // Sort by goal difference
            .ThenByDescending(c => c.GM)              // Sort by goals scored
            .Select((team, index) => new
            {
                Pozitie = index + 1,
                team.Echipa,
                team.M,
                team.V,
                team.E,
                team.I,
                team.GM,
                team.GP,
                team.P
            });


        return Json(clasament);
    }

    [HttpGet]
    public IActionResult GetRezultate(Guid grupaId, int etapa)
    {
        var meciuri = _service.GetMeciuriByGrupa(grupaId).Where(m => m.Etapa == etapa).ToList();
        return Json(meciuri);
    }

    [HttpGet]
    public IActionResult GetEtapaUrmatoare(Guid grupaId)
    {
        var meciuri = _service.GetMeciuriByGrupa(grupaId).Where(m => m.ScorGazde == null && m.ScorOaspeti == null && m.DataJoc != null).ToList();
        return Json(meciuri);
    }

    [HttpGet]
    public IActionResult GetEtapeByGrupa(Guid grupaId)
    {
        // Preia lista de etape pe baza meciurilor din grupa selectată

        var test = _service.GetMeciuriByGrupa(grupaId);
        var etape = _service.GetMeciuriByGrupa(grupaId)
            .Where(m => m.Etapa != 0)
            .Select(m => m.Etapa)
            .Distinct()
            .OrderBy(e => e)
            .ToList();

        return Json(etape);
    }

}
