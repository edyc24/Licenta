namespace AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models
{
    public class MeciModel
    {
        public Guid IdMeci { get; set; }
        public Guid? IdEchipaGazda { get; set; }
        public Guid? IdEchipaOaspete { get; set; }
        public DateTime? DataJoc { get; set; }
        public string? Rezultat { get; set; }
        public string? Observatii { get; set; }
        public byte[]? Raport { get; set; }
        public Guid? IdArbitru { get; set; }
        public Guid? IdArbitruAsistent1 { get; set; }
        public Guid? IdArbitruAsistent2 { get; set; }
        public Guid? IdArbitruRezerva { get; set; }
        public Guid? IdObservator { get; set; }
        public Guid? IdStadionLocalitate { get; set; }
        public bool? IdDeleted { get; set; }

        public string? EchipaGazdaNume { get; set; }
        public string? EchipaOaspeteNume { get; set; }
        public string? ArbitruNume { get; set; }
        public string? ArbitruAsistent1Nume { get; set; }
        public string? ArbitruAsistent2Nume { get; set; }
        public string? ArbitruRezervaNume { get; set; }
        public string? ObservatorNume { get; set; }
        public string? StadionNume { get; set; }
        public string? LocalitateNume { get; set; }
    }

}
