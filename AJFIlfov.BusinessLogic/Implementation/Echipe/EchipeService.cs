using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.EchipeService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.EchipeService
{
    public class EchipeService : BaseService
    {
        public EchipeService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<EchipaModel> GetAll()
        {
            var echipe = UnitOfWork.Echipe.Get().Select(e => new EchipaModel()
            {
                IdEchipa = e.IdEchipa,
                Nume = e.Nume
            }).ToList();

            return echipe;
        }

        public EchipaModel GetById(Guid id)
        {
            var echipa = UnitOfWork.Echipe.Find(id);
            if (echipa == null) return null;

            return new EchipaModel()
            {
                IdEchipa = echipa.IdEchipa,
                Nume = echipa.Nume
            };
        }

        public bool CreateEchipa(EchipaModel model)
        {
            var echipa = new Echipe()
            {
                IdEchipa = Guid.NewGuid(),
                Nume = model.Nume
            };

            UnitOfWork.Echipe.Insert(echipa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateEchipa(EchipaModel model)
        {
            var echipa = UnitOfWork.Echipe.Find(model.IdEchipa);
            if (echipa == null) return false;

            echipa.Nume = model.Nume;

            UnitOfWork.Echipe.Update(echipa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteEchipa(Guid id)
        {
            var echipa = UnitOfWork.Echipe.Find(id);
            if (echipa == null) return false;

            UnitOfWork.Echipe.Delete(echipa);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
