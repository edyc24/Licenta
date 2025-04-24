using System;
using System.ComponentModel.DataAnnotations;

namespace AJFIlfov.BusinessLogic.Implementation.TurneeService.Models
{
    public class CreateTurneuModel
    {
        [Required(ErrorMessage = "Data meciului este obligatorie")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Echipa gazdă este obligatorie")]
        public Guid? IdEchipaGazda { get; set; }

        [Required(ErrorMessage = "Echipa oaspete este obligatorie")]
        public Guid? IdEchipaOaspete { get; set; }

        [Required(ErrorMessage = "Stadionul este obligatoriu")]
        public Guid? IdStadion { get; set; }

        [Required(ErrorMessage = "Categoria este obligatorie")]
        public Guid? IdCategorie { get; set; }

        public Guid? IdGrupa { get; set; }

        public int? ScorGazda { get; set; }
        public int? ScorOaspeti { get; set; }

        public int? Runda { get; set; }
        public int? Index { get; set; }
    }
} 