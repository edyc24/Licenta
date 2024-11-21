using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Anunt : IEntity
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public string Continut { get; set; }
        public DateTime DataPublicarii { get; set; }

        // Adăugăm câmpul pentru imagine
        public byte[] Imagine { get; set; }
        public byte[]? ImagineAnunt { get; set; }
        public string TipAnunt { get; set; }
        public string PublishedBy { get; set; } // New property
        public int Views { get; set; } // New property
    }
}
