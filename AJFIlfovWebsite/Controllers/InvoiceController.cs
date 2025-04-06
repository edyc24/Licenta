using AJFIlfov.BusinessLogic.Implementation.Invoice;
using AJFIlfov.BusinessLogic.Implementation.Invoice.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.DataAccess;
using AJFIlfov.Entities.Entities;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace AJFIlfovWebsite.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(ControllerDependencies dependencies, InvoiceService invoiceService) : base(dependencies)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(InvoiceData invoiceData)
        {
            var pdfBytes = _invoiceService.CreateInvoice(invoiceData);
            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }
    }
}