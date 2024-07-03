using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Roluri : IEntity
{
    public int IdRol { get; set; }

    public string Nume { get; set; } = null!;

    public virtual ICollection<Utilizatori> Utilizatoris { get; set; } = new List<Utilizatori>();
}
