﻿namespace AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models
{ 
    public class MeciAdminModel
    {
        public Guid IdMeci { get; set; }
        public Guid? IdGrupa { get; set; }
        public Guid? IdEchipaGazda { get; set; }
        public Guid? IdEchipaOaspete { get; set; }

        public string? EchipaGazdaNume { get; set; }
        public string? EchipaOaspeteNume { get; set; }
        public DateTime? DataJoc { get; set; }
        public DateTime? OraJoc { get; set; }
        public Guid? IdArbitru { get; set; }
        public Guid? IdArbitruAsistent1 { get; set; }
        public Guid? IdArbitruAsistent2 { get; set; }
        public Guid? IdArbitruRezerva { get; set; }
        public Guid? IdObservator { get; set; }
        public Guid? IdStadionLocalitate { get; set; }

        public int? ScorGazde { get; set; }
        public int? ScorOaspeti { get; set; }
        public int? Etapa { get; set; }
        public string? Locatie { get; set; }
    }
    public class MeciArbitruModel
    {
        public Guid IdMeci { get; set; }
        public string? Rezultat { get; set; }
        public string? Observatii { get; set; }
        public byte[]? Raport { get; set; }
    }
}
