using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class PasswordRecovery : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public bool IsAvailable { get; set; }

    public string Code { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Utilizatori User { get; set; } = null!;
}
