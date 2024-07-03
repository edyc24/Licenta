using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Meciuri : IEntity
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

    public virtual Utilizatori? IdArbitruAsistent1Navigation { get; set; }

    public virtual Utilizatori? IdArbitruAsistent2Navigation { get; set; }

    public virtual Utilizatori? IdArbitruNavigation { get; set; }

    public virtual Utilizatori? IdArbitruRezervaNavigation { get; set; }

    public virtual GrupeEchipa? IdEchipaGazdaNavigation { get; set; }

    public virtual GrupeEchipa? IdEchipaOaspeteNavigation { get; set; }

    public virtual Utilizatori? IdObservatorNavigation { get; set; }

    public virtual StadionLocalitate? IdStadionLocalitateNavigation { get; set; }
}
