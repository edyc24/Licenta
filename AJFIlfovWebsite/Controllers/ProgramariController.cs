using AJFIlfov.BusinessLogic.Implementation.Account.Models;
using AJFIlfov.BusinessLogic.Implementation.Account;
using Microsoft.AspNetCore.Mvc;
using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using AJFIlfov.BusinessLogic.Implementation.Audituri;
using AJFIlfov.BusinessLogic.Implementation.Documente;
using AJFIlfov.BusinessLogic.Implementation.MeciLiveService;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;

namespace AJFIlfovWebsite.Controllers
{
    public class ProgramariController : BaseController
    {
        private readonly AccountService _accountService;

        public ProgramariController(
            ControllerDependencies dependencies,
            AccountService accountService)
            : base(dependencies)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AvailableSlots = _accountService.GetAvailableSlots(DateTime.Today.AddDays(1));
            return View(new CreateAppointmentModel());
        }

        [HttpPost]
        public IActionResult Index(CreateAppointmentModel model)
        {
            if (ModelState.IsValid)
            {
                _accountService.CreateAppointment(model);
                return RedirectToAction("Index","DespreNoi");
            }
            ViewBag.AvailableSlots = _accountService.GetAvailableSlots(model.Date);
            return View(model);
        }
    }
}
