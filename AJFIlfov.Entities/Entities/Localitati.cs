using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Localitati : IEntity
{
    public Guid IdLocalitate { get; set; }

    public string Nume { get; set; } = null!;
    public string Address { get; set; }

    public virtual ICollection<StadionLocalitate> StadionLocalitates { get; set; } = new List<StadionLocalitate>();
}
