using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Refereestat : IEntity
{
    public int? VarstaMedie { get; set; }

    public int? Total { get; set; }
}
