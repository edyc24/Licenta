using System;
using System.Collections.Generic;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public  class CategoriiTurneu : IEntity
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