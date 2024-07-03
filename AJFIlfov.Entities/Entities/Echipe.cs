using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Echipe : IEntity
{
    public Guid IdEchipa { get; set; }

    public string Nume { get; set; } = null!;

    public virtual ICollection<GrupeEchipa> GrupeEchipas { get; set; } = new List<GrupeEchipa>();
}
