using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.GrupeService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeService
{
    public class GrupeService : BaseService
    {
        public GrupeService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<GrupaModel> GetAll()
        {
            var grupe = UnitOfWork.Grupe.Get().Select(g => new GrupaModel()
            {
                IdGrupa = g.IdGrupa,
                Nume = g.Nume
            }).ToList();

            return grupe;
        }

        public GrupaModel GetById(Guid id)
        {
            var grupa = UnitOfWork.Grupe.Find(id);
            if (grupa == null) return null;

            return new GrupaModel()
            {
                IdGrupa = grupa.IdGrupa,
                Nume = grupa.Nume
            };
        }

        public bool CreateGrupa(GrupaModel model)
        {
            var grupa = new Grupe()
            {
                IdGrupa = Guid.NewGuid(),
                Nume = model.Nume
            };

            UnitOfWork.Grupe.Insert(grupa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateGrupa(GrupaModel model)
        {
            var grupa = UnitOfWork.Grupe.Find(model.IdGrupa);
            if (grupa == null) return false;

            grupa.Nume = model.Nume;

            UnitOfWork.Grupe.Update(grupa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteGrupa(Guid id)
        {
            var grupa = UnitOfWork.Grupe.Find(id);
            if (grupa == null) return false;

            UnitOfWork.Grupe.Delete(grupa);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}

