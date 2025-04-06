//using AJFIlfov.Code.Base;
//using AJFIlfov.WebApp.Code.Base;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService;
//using AJFIlfov.WebApp.Code.Base;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Models;

//namespace AJFIlfov.Controllers
//{
//    public class DisponibilitateController : BaseController
//    {
//        private readonly DisponibilitateService Service;

//        public DisponibilitateController(ControllerDependencies dependencies, DisponibilitateService service)
//           : base(dependencies)
//        {
//            this.Service = service;
//        }
//        public IActionResult Index()
//        {
//            var cereri = Service.GetCereri();
//            return View(cereri);
//        }

//        public IActionResult AdaugaDisponibilitate(CerereModel model)
//        {
//            var response = Service.Adauga(model);
//            return RedirectToAction("Index", "Disponibilitate");
//        }
//    }
//}
