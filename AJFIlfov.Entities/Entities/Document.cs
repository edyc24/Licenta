using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Document : IEntity
    {
        public int Id { get; set; }

        public string NumeDocument { get; set; }

        public byte[] PdfContent { get; set; } // Conținutul fișierului

        public string ContentType { get; set; } // Tipul MIME al fișierului (e.g., application/pdf)
    }
}
