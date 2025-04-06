using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService;
using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.Common;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using static PdfSharp.Pdf.AcroForms.PdfAcroField;


namespace AJFIlfovWebsite.Controllers;

[Authorize]
public class MeciuriController : BaseController
{
    private readonly DisponibilitateAdminService _disService;
    private readonly GrupeService _grupeService;
    private readonly MatchReportService _matchReportService;
    private readonly MeciuriService _service;

    public MeciuriController(ControllerDependencies dependencies, MeciuriService service, GrupeService grupeService,
        DisponibilitateAdminService disService, MatchReportService matchReportService)
        : base(dependencies)
    {
        _service = service;
        _grupeService = grupeService;
        _disService = disService;
        _matchReportService = matchReportService;
    }

    public IActionResult Index()
    {
        var meciuri = _service.GetAll();
        return View(meciuri);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Grupe = _grupeService.GetAll();
        ViewBag.Arbitri = _service.GetAllArbitriAll();
        ViewBag.Stadioane = _service.GetAllStadioane();
        ViewBag.Echipe = new List<TeamModel>();
        return View();
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public IActionResult Create(MeciAdminModel model)
    {
        if (ModelState.IsValid)
        {
            _service.CreateMeci(model);
            return RedirectToAction("Index");
        }

        ViewBag.Grupe = _grupeService.GetAll();
        ViewBag.Arbitri = _service.GetAllArbitriAll();
        ViewBag.Stadioane = _service.GetAllStadioane();
        ViewBag.Echipe = new List<TeamModel>();
        return View(model);
    }

    public IActionResult GetDisponibilitati(DateTime data)
    {
        var disponibilitati =
            _disService.GetRefereesAvailabilityByDay(data); // Your method to get the availability data
        return PartialView("_AvailabilityTable", disponibilitati);
    }


    [HttpGet]
    public IActionResult GetTeamsByGrupa(Guid grupaId)
    {
        var teams = _service.GetTeamsByGrupa(grupaId);
        return Json(teams);
    }

    // Arbitru actions
    [Authorize(Roles = "Arbitru,Administrator")]
    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var meci = _service.GetById(id);
        if (meci == null) return NotFound();

        var model = new MeciArbitruModel
        {
            IdMeci = meci.IdMeci,
            Rezultat = meci.Rezultat,
            Observatii = meci.Observatii,
            Raport = meci.Raport
        };

        return View(model);
    }

    [Authorize(Roles = "Arbitru,Administrator")]
    [HttpPost]
    public async Task<IActionResult> Edit(MeciArbitruModel meciArbitruModel, IFormFile raport)
    {
        var pdfConverter = new PdfConverter();
        var raportBytes = await pdfConverter.ConvertPdfAsync(raport);

        _service.UpdateMeciByArbitru(meciArbitruModel, raportBytes);
        return RedirectToAction("Index");
    }

    public IActionResult DownloadRaport(Guid id)
    {
        var meci = _service.GetById(id);
        if (meci == null || meci.Raport == null) return NotFound();

        var fileName = $"Raport_Meci_{meci.EchipaGazdaNume}_vs_{meci.EchipaOaspeteNume}_{meci.DataJoc}.pdf";
        return File(meci.Raport, "application/pdf", fileName);
    }

    public IActionResult IstoricMeciuri()
    {
        var pastMeciuri = _service.GetAllCurrentUser().Where(m => m.DataJoc < DateTime.UtcNow);

        return View(pastMeciuri);
    }


    // Edit for Admins
    [HttpGet]
    public IActionResult EditareMeciAdmin(Guid id)
    {
        var model = _service.GetMeciAdminById(id);
        if (model == null) return NotFound();

        ViewBag.Grupe = _grupeService.GetAll();
        ViewBag.Arbitri = _service.GetAllArbitri(model.DataJoc);
        ViewBag.Stadioane = _service.GetAllStadioane();
        ViewBag.Echipe = _service.GetAllTeams();
        return View("EditareMeciAdmin", model);
    }

    [HttpPost]
    public IActionResult EditareMeciAdmin(MeciAdminModel model)
    {
        if (ModelState.IsValid)
        {
            _service.UpdateMeciByAdmin(model);
            return RedirectToAction("Index");
        }

        ViewBag.Grupe = _grupeService.GetAll();
        ViewBag.Arbitri = _service.GetAllArbitri(model.DataJoc);
        ViewBag.Stadioane = _service.GetAllStadioane();
        ViewBag.Echipe = new List<TeamModel>();
        return View("EditareMeciAdmin", model);
    }


    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var meci = _service.GetById(id);
        if (meci == null) return NotFound();

        return View(meci);
    }

    // Delete action for both roles
    [Authorize(Roles = "Administrator,Arbitru")]
    [HttpPost]
    public IActionResult DeleteConfirmed(Guid id)
    {
        _service.DeleteMeci(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult GetAddressData(Guid refereeId, Guid stadiumId)
    {
        var referee = _service.GetRefAddress(refereeId);
        var stadium = _service.GetStadiumAddress(stadiumId);

        if (referee == null || stadium == null) return NotFound("Referee or Stadium not found.");

        return Json(new
        {
            refereeAddress = referee,
            stadiumAddress = stadium
        });
    }

    [HttpPost]
    public IActionResult GenerateReport(Guid matchId)
    {
        var match = _service.GetById(matchId); // Assume this method retrieves the match data

        var reportData = new MatchReportData
        {
            MatchLocation = match.LocalitateNume ?? "-",
            MatchDate = ConvertDateTimeToString(match.DataJoc),
            StadiumName = match.StadionNume ?? "-",
            RefereeName = match.ArbitruNume ?? "-",
            AssistantReferee1 = match.ArbitruAsistent1Nume ?? "-",
            AssistantReferee2 = match.ArbitruAsistent2Nume ?? "-",
            HomeTeam = match.EchipaGazdaNume ?? "-",
            AwayTeam = match.EchipaOaspeteNume ?? "-"
        };

        var path = "C:\\Users\\eduardcr\\Source\\Repos\\edyc24\\Licenta\\AJFIlfovWebsite\\wwwroot\\raport.pdf";

        if (System.IO.File.Exists(path))
        {
            var sourceFile = System.IO.File.OpenRead(path);
            var output = new MemoryStream();

            try
            {
                var pdfDocument = PdfReader.Open(sourceFile, PdfDocumentOpenMode.Modify);
                var form = pdfDocument.AcroForm;

                if (form != null)
                {
                    SetField(form.Fields, "Localitate", reportData.MatchLocation);
                    //SetField(form.Fields, "Competitia", reportData.MatchLocation);
                    SetField(form.Fields, "Localitate1", "Ilfov");
                    SetField(form.Fields, "Localitate2", "Ilfov");
                    SetField(form.Fields, "Localitate3", "Ilfov");
                    SetField(form.Fields, "Data", reportData.MatchDate);
                    SetField(form.Fields, "Jocul", reportData.HomeTeam + '-' + reportData.AwayTeam);
                    SetField(form.Fields, "Stadion", reportData.StadiumName);
                    SetField(form.Fields, "Arbitru", reportData.RefereeName);
                    SetField(form.Fields, "ArbitruAsistent1", reportData.AssistantReferee1);
                    SetField(form.Fields, "ArbitruAsistent2", reportData.AssistantReferee2);
                    SetField(form.Fields, "EchipaGazda", reportData.HomeTeam);
                    SetField(form.Fields, "EchipaOaspete", reportData.AwayTeam);

                    pdfDocument.Save(output);
                    byte[] bytes = output.ToArray();

                    return File(bytes, "application/pdf", "raport.pdf");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        return View("Index");
    }

    private void SetField(PdfAcroFieldCollection fields, string fieldName, string value)
    {
        if (fields[fieldName] is PdfTextField textField)
        {
            textField.Value = new PdfString(value);
        }
    }




    private string ConvertDateTimeToString(DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToString("yyyy-MM-dd") : "-";
    }



    public async Task<IActionResult> EditareMeciuriGrupa(Guid idGrupa)
    {
        idGrupa = Guid.Parse("e2d0e2f0-ff68-4108-9811-0ab4d4285155");
        // Preia toate meciurile din grupa selectată
        var meciuri = _service.GetMeciuriByGrupa(idGrupa);

        // Populează ViewBag cu datele necesare pentru select lists
        ViewBag.Arbitri = _service.GetAllArbitriAll();
        ViewBag.Grupe = _service.GetAllGrupe();
        ViewBag.Stadioane = _service.GetAllStadioane();

        return View(meciuri); // Trimite lista de meciuri către view
    }

    [HttpPost]
    public async Task<IActionResult> EditareMeciuriGrupa([FromForm] List<MeciAdminModel> Meciuri)
    {
        if (ModelState.IsValid)
        {
            // Update the data based on the form submission
            _service.UpdateMeciuriByAdmin(Meciuri);

            // Redirect to a confirmation page or back to the list
            return RedirectToAction("Index");
        }

        // If the model state is not valid, repopulate the ViewBag and return the view with the models
        ViewBag.Arbitri = _service.GetAllArbitriAll();
        ViewBag.Grupe = _service.GetAllGrupe();
        ViewBag.Stadioane = _service.GetAllStadioane();

        return View(Meciuri); // Return the view with the models to display validation errors
    }
}