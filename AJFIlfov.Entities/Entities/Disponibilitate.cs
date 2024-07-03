using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Disponibilitate : IEntity
{
    public Guid IdDisponibilitate { get; set; }

    public Guid? IdUtilizator { get; set; }

    public DateTime? Zi { get; set; }

    public string? Status { get; set; }

    public DateTime? OraIncepere { get; set; }

    public DateTime? OraIncheiere { get; set; }

    public virtual Utilizatori? IdUtilizatorNavigation { get; set; }
}
