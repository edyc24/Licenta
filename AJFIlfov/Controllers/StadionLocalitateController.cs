using AJFIlfov.BusinessLogic.Implementation.LocalitatiService;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfov.Controllers
{
    public class StadionLocalitateController : BaseController
    {
        private readonly StadionLocalitateService _service;
        private readonly StadioaneService _stadioaneService;
        private readonly LocalitatiService _localitatiService;

        public StadionLocalitateController(ControllerDependencies dependencies, StadionLocalitateService service, StadioaneService stadioaneService, LocalitatiService localitatiService)
            : base(dependencies)
        {
            _service = service;
            _stadioaneService = stadioaneService;
            _localitatiService = localitatiService;
        }

        public IActionResult Index()
        {
            var stadionLocalitate = _service.GetAll();
            return View(stadionLocalitate);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Stadioane = _stadioaneService.GetAll();
            ViewBag.Localitati = _localitatiService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(StadionLocalitateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateStadionLocalitate(model);
                return RedirectToAction("Index");
            }
            ViewBag.Stadioane = _stadioaneService.GetAll();
            ViewBag.Localitati = _localitatiService.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var stadionLocalitate = _service.GetById(id);
            if (stadionLocalitate == null) return NotFound();

            ViewBag.Stadioane = _stadioaneService.GetAll();
            ViewBag.Localitati = _localitatiService.GetAll();
            return View(stadionLocalitate);
        }

        [HttpPost]
        public IActionResult Edit(StadionLocalitateModel model)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateStadionLocalitate(model);
                return RedirectToAction("Index");
            }
            ViewBag.Stadioane = _stadioaneService.GetAll();
            ViewBag.Localitati = _localitatiService.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var stadionLocalitate = _service.GetById(id);
            if (stadionLocalitate == null) return NotFound();

            return View(stadionLocalitate);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteStadionLocalitate(id);
            return RedirectToAction("Index");
        }
    }
}
