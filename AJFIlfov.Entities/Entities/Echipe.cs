using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Echipe : IEntity
{
    public Echipe()
    {
        GrupeEchipas = new HashSet<GrupeEchipa>();
        MeciuriIdEchipaGazdaNavigations = new HashSet<Meciuri>();
        MeciuriIdEchipaOaspeteNavigations = new HashSet<Meciuri>();
        TurneeIdEchipaGazdaNavigation = new HashSet<Turnee>();
        TurneeIdEchipaOaspeteNavigation = new HashSet<Turnee>();
    }

    public Guid IdEchipa { get; set; }

    public string Nume { get; set; } = null!;

    public virtual ICollection<GrupeEchipa> GrupeEchipas { get; set; } = new List<GrupeEchipa>();
    public virtual ICollection<Meciuri> MeciuriIdEchipaGazdaNavigations { get; set; } = new List<Meciuri>();
    public virtual ICollection<Meciuri> MeciuriIdEchipaOaspeteNavigations { get; set; } = new List<Meciuri>();
    public virtual ICollection<Turnee> TurneeIdEchipaGazdaNavigation { get; set; } = new List<Turnee>();
    public virtual ICollection<Turnee> TurneeIdEchipaOaspeteNavigation { get; set; } = new List<Turnee>();
}
