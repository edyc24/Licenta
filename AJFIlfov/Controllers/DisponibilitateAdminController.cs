﻿//using AJFIlfov.Code.Base;
//using AJFIlfov.WebApp.Code.Base;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService;
//using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace AJFIlfov.Controllers
//{
//    //[Authorize(Roles = "Administrator")]
//    public class DisponibilitateAdminController : BaseController
//    {
//        private readonly DisponibilitateAdminService Service;

//        public DisponibilitateAdminController(ControllerDependencies dependencies, DisponibilitateAdminService service)
//           : base(dependencies)
//        {
//            this.Service = service;
//        }
//        public IActionResult Index()
//        {
//            var cereri = Service.GetAllAvailable();
//            return View(cereri);
//        }
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Create(CerereDisponibilitateModel model)
//        {
//            Service.CreateDisponibilitate(model);
//            return RedirectToAction("Index", "DisponibilitateAdmin");
//        }

//        public IActionResult VeziRezultate(DateTime zi)
//        {
//            // Fetch the list of referees and their availability
//            var referees = Service.GetRefereesAvailabilityByDay(zi);
//            ViewBag.SelectedDay = zi;
//            return View(referees);
//        }
//    }
//}
