using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class StadionLocalitate : IEntity
{
    public Guid IdStadionLocalitate { get; set; }

    public Guid? IdStadion { get; set; }

    public Guid? IdLocalitate { get; set; }

    public virtual Localitati? IdLocalitateNavigation { get; set; }

    public virtual Stadioane? IdStadionNavigation { get; set; }

    public virtual ICollection<Meciuri> Meciuris { get; set; } = new List<Meciuri>();
}
