using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService;
using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
using AJFIlfov.Code.Base;
using AJFIlfov.Common;
using AJFIlfov.DataAccess;
using AJFIlfov.WebApp.Code.Base;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Collections.Generic;
using System.Web;
using Org.BouncyCastle.Utilities;
using iText.Kernel.Exceptions;
using iText.Pdfa.Checker;
using iText.Pdfa.Exceptions;


namespace AJFIlfov.Controllers
{
    [Authorize]
    public class MeciuriController : BaseController
    {
        private readonly MeciuriService _service;
        private readonly GrupeService _grupeService;
        private readonly DisponibilitateAdminService _disService;
        private readonly MatchReportService _matchReportService;

        public MeciuriController(ControllerDependencies dependencies, MeciuriService service, GrupeService grupeService, DisponibilitateAdminService disService, MatchReportService matchReportService)
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
            var disponibilitati = _disService.GetRefereesAvailabilityByDay(data); // Your method to get the availability data
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
            byte[] raportBytes = await pdfConverter.ConvertPdfAsync(raport);

            _service.UpdateMeciByArbitru(meciArbitruModel, raportBytes);
            return RedirectToAction("Index");
        }

        public IActionResult DownloadRaport(Guid id)
        {
            var meci = _service.GetById(id);
            if (meci == null || meci.Raport == null)
            {
                return NotFound();
            }

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
            if (model == null)
            {
                return NotFound();
            }

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

            if (referee == null || stadium == null)
            {
                return NotFound("Referee or Stadium not found.");
            }

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
                MatchLocation = match.LocalitateNume,
                MatchDate = match.DataJoc.Value,
                StadiumName = match.StadionNume,
                RefereeName = match.ArbitruNume,
                AssistantReferee1 = match.ArbitruAsistent1Nume,
                AssistantReferee2 = match.ArbitruAsistent2Nume,
                HomeTeam = match.EchipaGazdaNume,
                AwayTeam = match.EchipaOaspeteNume,
            };

            string path = "C:\\Users\\eduard.cristea\\source\\repos\\Licenta\\AJFIlfov\\wwwroot\\raport.pdf";


            if (System.IO.File.Exists(path))
            {
                var sourceFile = System.IO.File.OpenRead(path);
                var output = new MemoryStream();


                try
                {
                    using var pdfDocument = new PdfDocument(new PdfReader(sourceFile));

                    PdfAChecker pdfAChecker = new PdfAChecker(PdfAConformanceLevel.PDF_A_1B);
                    pdfAChecker.CheckPdfAConformance(pdfDocument);

                    Console.WriteLine("PDF-ul este conform standardului PDF/A.");
                }
                catch (PdfAConformanceException ex)
                {
                    Console.WriteLine("PDF-ul nu este conform standardului PDF/A: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("O eroare neașteptată a apărut: " + ex.Message);
                }

                //var pdf = new PdfDocument(new PdfReader(sourceFile), new PdfWriter(output));
                //PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, false);

                //if(form != null)
                //{
                //    IDictionary<String, PdfFormField> fields = form.GetAllFormFields();
                //    PdfFormField toset;

                //    fields.TryGetValue("Jocul", out toset);
                //    toset.SetValue("test");
                //    pdf.Close();
                //    byte[] bytes = output.ToArray();



                //    return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet);
                //}
            }

            return View("Index");

        }

    }
}
