using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Stadioane : IEntity
{
    public Guid IdStadion { get; set; }

    public string Nume { get; set; } = null!;

    public virtual ICollection<StadionLocalitate> StadionLocalitates { get; set; } = new List<StadionLocalitate>();
    public virtual ICollection<Turnee> Turnee { get; set; } = new List<Turnee>();
}
