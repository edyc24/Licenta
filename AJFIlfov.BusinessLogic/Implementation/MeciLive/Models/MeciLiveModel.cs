using System;
using System.ComponentModel.DataAnnotations;

namespace AJFIlfov.BusinessLogic.Implementation.MeciLiveService.Models
{
    public class MeciLiveModel
    {
        public int IdMeciLive { get; set; }

        [Required(ErrorMessage = "Echipa gazdă este obligatorie")]
        public string EchipaGazda { get; set; }

        [Required(ErrorMessage = "Echipa oaspete este obligatorie")]
        public string EchipaOaspete { get; set; }

        [Range(0, int.MaxValue)]
        public int ScorGazda { get; set; }

        [Range(0, int.MaxValue)]
        public int ScorOaspete { get; set; }

        [Required]
        public string Status { get; set; } // "Programat", "InDesfasurare", "Pauza", "Finalizat"

        [Required(ErrorMessage = "Data și ora meciului sunt obligatorii")]
        public DateTime DataOra { get; set; }

        // Proprietăți noi
        public int? Minut { get; set; } // Minutul curent al meciului
        public DateTime? StartTime { get; set; } // Timpul de start al meciului
        public bool IsSecondHalf { get; set; } // Indicator pentru repriza a doua
    }
}