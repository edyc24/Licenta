namespace AJFIlfov.BusinessLogic.Implementation.TurneeService.Models
{
    public class TurneuModel
    {
        public Guid IdTurneu { get; set; }
        public DateTime? Data { get; set; }
        public Guid? IdEchipaGazda { get; set; }
        public Guid? IdEchipaOaspete { get; set; }
        public Guid? IdStadion { get; set; }
        public Guid? IdCategorie { get; set; }
        public Guid? IdGrupa { get; set; }
        public int? ScorGazda { get; set; }
        public int? ScorOaspeti { get; set; }
        public int? Index { get; set; }
        public int? Runda { get; set; }

        public string? EchipaGazdaNume { get; set; }
        public string? EchipaOaspeteNume { get; set; }
        public string? StadionNume { get; set; }
        public string? CategorieNume { get; set; }
        public string? GrupaNume { get; set; }
    }
} 