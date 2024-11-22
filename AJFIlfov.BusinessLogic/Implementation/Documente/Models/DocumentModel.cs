// BusinessLogic.Implementation.AnuntService.Models/AnuntModel.cs
using System.ComponentModel.DataAnnotations;

namespace AJFIlfov.BusinessLogic.Implementation.Documente.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }

        public string NumeDocument { get; set; }

        public byte[] PdfContent { get; set; } // Conținutul fișierului

        public string ContentType { get; set; } // Tipul MIME al fișierului (e.g., application/pdf)
    }
}