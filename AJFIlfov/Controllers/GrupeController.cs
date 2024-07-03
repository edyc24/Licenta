using AJFIlfov.BusinessLogic.Implementation.GrupeService;
using AJFIlfov.BusinessLogic.Implementation.GrupeService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfov.Controllers
{
    public class GrupeController : BaseController
    {
        private readonly GrupeService _service;

        public GrupeController(ControllerDependencies dependencies, GrupeService service)
            : base(dependencies)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var grupe = _service.GetAll();
            return View(grupe);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GrupaModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateGrupa(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var grupa = _service.GetById(id);
            if (grupa == null) return NotFound();

            return View(grupa);
        }

        [HttpPost]
        public IActionResult Edit(GrupaModel model)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateGrupa(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var grupa = _service.GetById(id);
            if (grupa == null) return NotFound();

            return View(grupa);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteGrupa(id);
            return RedirectToAction("Index");
        }
    }
}
