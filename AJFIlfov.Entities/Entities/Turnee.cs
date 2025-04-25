using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Turnee : IEntity
{
    public Guid IdTurneu { get; set; }
    public DateTime? Data { get; set; }
    public Guid? IdEchipaGazda { get; set; }
    public Guid? IdEchipaOaspete { get; set; }
    public Guid? IdStadion { get; set; }
    public Guid? IdCategorie { get; set; }
    public Guid? IdGrupa { get; set; }
    public int? ScorGazda { get; set; }
    public int? ScorOaspeti { get; set; }
    public int? Runda { get; set; }
    public int? Index { get; set; }
    public bool? IdDeleted { get; set; }
    public bool? Processed { get; set; }
    public int? ProcessedValue { get; set; }

    public virtual Echipe? IdEchipaGazdaNavigation { get; set; }
    public virtual Echipe? IdEchipaOaspeteNavigation { get; set; }
    public virtual Stadioane? IdStadionNavigation { get; set; }
    public virtual CategoriiTurneu? IdCategorieNavigation { get; set; }
    public virtual Grupe? IdGrupaNavigation { get; set; }
} 