using AJFIlfov.BusinessLogic.Implementation.TurneeService;
using AJFIlfov.BusinessLogic.Implementation.TurneeService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers
{
    [Authorize]
    public class TurneeController : BaseController
    {
        private readonly TurneeService _service;

        public TurneeController(ControllerDependencies dependencies, TurneeService service)
            : base(dependencies)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var turnee = _service.GetAll();
            return View(turnee);
        }

        [HttpGet]
        public IActionResult GroupStage(string categorie)
        {
            // Get all matches for the specified category
            var meciuri = _service.GetMeciuriByCategorie(categorie);
            
            // Calculate group standings
            var grupe = _service.CalculateGroupStandings(meciuri);
            
            // Get upcoming matches
            var upcomingMatches = _service.GetUpcomingMatches(categorie);

            ViewBag.Categorie = categorie;
            ViewBag.Grupe = grupe;
            ViewBag.UpcomingMatches = upcomingMatches;

            return View();
        }

        [HttpGet]
        public IActionResult EliminationStage(string categorie)
        {
            // Get all matches for the specified category
            var meciuri = _service.GetMeciuriByCategorie(categorie);
            
            // Get elimination stage matches
            var eliminationMatches = _service.GetEliminationStageMatches(meciuri);

            ViewBag.Categorie = categorie;
            ViewBag.EliminationMatches = eliminationMatches;

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TurneuModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var turneu = _service.GetById(id);
            if (turneu == null)
            {
                return NotFound();
            }

            return View(turneu);
        }

        [HttpPost]
        public IActionResult Edit(TurneuModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var turneu = _service.GetById(id);
            if (turneu == null)
            {
                return NotFound();
            }

            return View(turneu);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
