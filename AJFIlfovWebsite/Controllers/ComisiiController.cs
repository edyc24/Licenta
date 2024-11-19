using AJFIlfov.BusinessLogic.Implementation.Anunturi;
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace AJFIlfovWebsite.Controllers;

public class ComisiiController : Controller
{
    private readonly AnuntService _anuntService;

    public ComisiiController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    public IActionResult Index(string searchText = "", string sectionFilter = "all")
    {
        var anunturi = _anuntService.GetAnunturiComisii();

        if (!string.IsNullOrEmpty(searchText))
        {
            searchText = searchText.ToLower();

            // Filter announcements based on PDF content at runtime
            var filteredAnunturi = new List<AnuntModel>();

            foreach (var anunt in anunturi)
            {
                bool matches = false;

                // Search in title and content
                if ((anunt.Titlu != null && anunt.Titlu.ToLower().Contains(searchText)) ||
                    (anunt.Continut != null && anunt.Continut.ToLower().Contains(searchText)))
                {
                    matches = true;
                }
                else if (anunt.Imagine != null && anunt.Imagine.Length > 0)
                {
                    // Extract text from PDF
                    string pdfText = ExtractTextFromPdf(anunt.Imagine);

                    // Search in extracted PDF text
                    if (pdfText != null && pdfText.ToLower().Contains(searchText))
                    {
                        matches = true;
                    }
                }

                if (matches)
                {
                    filteredAnunturi.Add(anunt);
                }
            }

            anunturi = filteredAnunturi;
        }

        if (sectionFilter != "all")
        {
            anunturi = anunturi.Where(a => a.TipAnunt == sectionFilter).ToList();
        }

        return View(anunturi);
    }

    // Helper method to extract text from PDF byte array
    private string ExtractTextFromPdf(byte[] pdfBytes)
    {
        using (var pdfStream = new MemoryStream(pdfBytes))
        {
            using (var pdfDocument = PdfDocument.Open(pdfStream))
            {
                StringBuilder textBuilder = new StringBuilder();

                foreach (Page page in pdfDocument.GetPages())
                {
                    textBuilder.AppendLine(page.Text);
                }

                return textBuilder.ToString();
            }
        }
    }
}