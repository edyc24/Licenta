//using AJFIlfov.BusinessLogic.Implementation.StadioaneService;
//using AJFIlfov.BusinessLogic.Implementation.StadioaneService.Models;
//using AJFIlfov.Code.Base;
//using AJFIlfov.WebApp.Code.Base;
//using Microsoft.AspNetCore.Mvc;

//namespace AJFIlfov.Controllers
//{
//    public class StadioaneController : BaseController
//    {
//        private readonly StadioaneService _service;

//        public StadioaneController(ControllerDependencies dependencies, StadioaneService service)
//            : base(dependencies)
//        {
//            _service = service;
//        }

//        public IActionResult Index()
//        {
//            var stadioane = _service.GetAll();
//            return View(stadioane);
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(StadionModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                _service.CreateStadion(model);
//                return RedirectToAction("Index");
//            }
//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult Edit(Guid id)
//        {
//            var stadion = _service.GetById(id);
//            if (stadion == null) return NotFound();

//            return View(stadion);
//        }

//        [HttpPost]
//        public IActionResult Edit(StadionModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                _service.UpdateStadion(model);
//                return RedirectToAction("Index");
//            }
//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult Delete(Guid id)
//        {
//            var stadion = _service.GetById(id);
//            if (stadion == null) return NotFound();

//            return View(stadion);
//        }

//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeleteConfirmed(Guid id)
//        {
//            _service.DeleteStadion(id);
//            return RedirectToAction("Index");
//        }
//    }
//}
