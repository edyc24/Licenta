using System;
using System.Collections.Generic;

namespace AJFIlfov.Entities.Entities
{
    public partial class CategoriiTurneu
    {
        public CategoriiTurneu()
        {
            Turnee = new HashSet<Turnee>();
        }

        public Guid IdCategorie { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public bool? IdDeleted { get; set; }

        public virtual ICollection<Turnee> Turnee { get; set; }
    }
} 