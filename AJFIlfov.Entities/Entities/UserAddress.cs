using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class UserAddress : IEntity
{
    public Guid UserId { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual Utilizatori User { get; set; } = null!;
}
