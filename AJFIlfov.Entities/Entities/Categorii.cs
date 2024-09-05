using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Categorii : IEntity
{
    public int Id { get; set; }

    public string Categorie { get; set; } = null!;

    public virtual ICollection<Utilizatori> Utilizatories { get; set; } = new List<Utilizatori>();
}
