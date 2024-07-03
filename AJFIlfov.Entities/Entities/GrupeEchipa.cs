using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class GrupeEchipa : IEntity
{
    public Guid IdGrupaEchipa { get; set; }

    public Guid IdEchipa { get; set; }

    public Guid IdGrupa { get; set; }

    public virtual Echipe? IdEchipaNavigation { get; set; }

    public virtual Grupe? IdGrupaNavigation { get; set; }

    public virtual ICollection<Meciuri> MeciuriIdEchipaGazdaNavigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<Meciuri> MeciuriIdEchipaOaspeteNavigations { get; set; } = new List<Meciuri>();
}
