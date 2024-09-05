using AJFIlfov.Common;
using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities;

public partial class Utilizatori : IEntity
{
    public Guid IdUtilizator { get; set; }

    public string Nume { get; set; } = null!;

    public string Prenume { get; set; } = null!;

    public string? Adresa { get; set; }

    public string? Mail { get; set; }

    public string? Parola { get; set; }
    public string? NumarTelefon { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime? DataIncepere { get; set; }

    public DateTime? DataNastere { get; set; }

    public int IdRol { get; set; }
    public int IdCategorie { get; set; }

    public int? IdMarimeAdidasi { get; set; }

    public int? IdMarimeHaine { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsSuspended { get; set; }

    public virtual ICollection<Disponibilitate> Disponibilitates { get; set; } = new List<Disponibilitate>();

    public virtual Marimi? IdMarimeAdidasiNavigation { get; set; }

    public virtual Marimi? IdMarimeHaineNavigation { get; set; }

    public virtual Roluri IdRolNavigation { get; set; } = null!;
    public virtual Categorii IdCategorieNavigation { get; set; } = null!;

    public virtual ICollection<Meciuri> MeciuriIdArbitruAsistent1Navigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<Meciuri> MeciuriIdArbitruAsistent2Navigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<Meciuri> MeciuriIdArbitruNavigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<Meciuri> MeciuriIdArbitruRezervaNavigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<Meciuri> MeciuriIdObservatorNavigations { get; set; } = new List<Meciuri>();

    public virtual ICollection<PasswordRecovery> PasswordRecoveries { get; set; } = new List<PasswordRecovery>();

    public virtual UserAddress? UserAddress { get; set; }
}
