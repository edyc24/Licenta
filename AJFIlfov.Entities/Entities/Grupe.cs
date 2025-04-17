using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Grupe : IEntity
{
    public Guid IdGrupa { get; set; }

    public string Nume { get; set; } = null!;

    public virtual ICollection<GrupeEchipa> GrupeEchipas { get; set; } = new List<GrupeEchipa>();
    public virtual ICollection<Turnee> Turnee { get; set; } = new List<Turnee>();
}
