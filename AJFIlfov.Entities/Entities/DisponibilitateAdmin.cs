using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class DisponibilitateAdmin : IEntity
{
    public Guid IdDisponibilitateAdmin { get; set; }

    public DateTime Zi { get; set; }

    public DateTime CreatedOn { get; set; }
}
