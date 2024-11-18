using System;
using System.ComponentModel.DataAnnotations;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class MeciLive :IEntity
    {
        [Key]
        public int IdMeciLive { get; set; }

        [Required]
        public string EchipaGazda { get; set; }

        [Required]
        public string EchipaOaspete { get; set; }

        public int ScorGazda { get; set; }
        public int ScorOaspete { get; set; }

        [Required]
        public string Status { get; set; } // "Programat", "InDesfasurare", "Pauza", "Finalizat"

        [Required]
        public DateTime DataOra { get; set; }

        // Proprietăți noi
        public int? Minut { get; set; } // Minutul curent al meciului
        public DateTime? StartTime { get; set; } // Timpul de start al meciului
        public bool IsSecondHalf { get; set; } // Indicator pentru repriza a doua
    }
}