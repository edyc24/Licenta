using AJFIlfov.BusinessLogic.Implementation.LocalitatiService;
using AJFIlfov.BusinessLogic.Implementation.LocalitatiService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfov.Controllers
{
    public class LocalitatiController : BaseController
    {
        private readonly LocalitatiService _service;

        public LocalitatiController(ControllerDependencies dependencies, LocalitatiService service)
            : base(dependencies)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var localitati = _service.GetAll();
            return View(localitati);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocalitateModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateLocalitate(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var localitate = _service.GetById(id);
            if (localitate == null) return NotFound();

            return View(localitate);
        }

        [HttpPost]
        public IActionResult Edit(LocalitateModel model)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateLocalitate(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var localitate = _service.GetById(id);
            if (localitate == null) return NotFound();

            return View(localitate);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteLocalitate(id);
            return RedirectToAction("Index");
        }
    }
}
