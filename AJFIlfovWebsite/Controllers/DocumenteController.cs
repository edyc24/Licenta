using AJFIlfov.BusinessLogic.Implementation.Documente;
using AJFIlfov.BusinessLogic.Implementation.Documente.Models;

using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers
{
    public class DocumenteController : Controller
    {
        private readonly DocumenteService _service;

        public DocumenteController(DocumenteService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var documente = _service.GetAllADocumente().ToList();
            ViewBag.Documente = documente;
            return View();
        }


        public IActionResult Display(int id)
        {
            var document = _service.GetAllADocumente().FirstOrDefault(d => d.Id == id);
            if (document == null) return NotFound();

            return View(document);
        }

        public IActionResult Download(int id)
        {
            var document = _service.GetAllADocumente().FirstOrDefault( d => d.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            var fileStream = new MemoryStream(document.PdfContent);
            var contentType = "application/pdf";
            var fileName = $"{document.NumeDocument}.pdf";

            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(fileStream, contentType);
        }

    }
}
