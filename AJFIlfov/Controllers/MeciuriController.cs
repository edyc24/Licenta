using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
using AJFIlfov.Code.Base;
using AJFIlfov.DataAccess;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AJFIlfov.Controllers
{
    [Authorize]
    public class MeciuriController : BaseController
    {
        private readonly MeciuriService _service;
        private readonly GrupeService _grupeService;

        public MeciuriController(ControllerDependencies dependencies, MeciuriService service, GrupeService grupeService)
            : base(dependencies)
        {
            _service = service;
            _grupeService = grupeService;
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
            ViewBag.Arbitri = _service.GetAllArbitri();
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
            ViewBag.Arbitri = _service.GetAllArbitri();
            ViewBag.Stadioane = _service.GetAllStadioane();
            return View(model);
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
        public IActionResult Edit(MeciArbitruModel model)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateMeciByArbitru(model);
                return RedirectToAction("Index");
            }
            return View(model);
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
            ViewBag.Arbitri = _service.GetAllArbitri();
            ViewBag.Stadioane = _service.GetAllStadioane();
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
            ViewBag.Arbitri = _service.GetAllArbitri();
            ViewBag.Stadioane = _service.GetAllStadioane();
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

    }
}
