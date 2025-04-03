using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers
{
    public class TurneeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GroupStage()
        {
            return View();
        }

        public IActionResult EliminationStage()
        {
            return View();
        }
    }
}
