using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Marimi : IEntity
{
    public int IdMarime { get; set; }

    public string? Marime { get; set; }

    public int? Tip { get; set; }

    public virtual ICollection<Utilizatori> UtilizatoriIdMarimeAdidasiNavigations { get; set; } = new List<Utilizatori>();

    public virtual ICollection<Utilizatori> UtilizatoriIdMarimeHaineNavigations { get; set; } = new List<Utilizatori>();
}
