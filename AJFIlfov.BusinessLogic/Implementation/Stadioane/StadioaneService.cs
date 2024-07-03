using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.StadioaneService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.StadioaneService
{
    public class StadioaneService : BaseService
    {
        public StadioaneService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<StadionModel> GetAll()
        {
            var stadioane = UnitOfWork.Stadioane.Get().Select(s => new StadionModel()
            {
                IdStadion = s.IdStadion,
                Nume = s.Nume
            }).ToList();

            return stadioane;
        }

        public StadionModel GetById(Guid id)
        {
            var stadion = UnitOfWork.Stadioane.Find(id);
            if (stadion == null) return null;

            return new StadionModel()
            {
                IdStadion = stadion.IdStadion,
                Nume = stadion.Nume
            };
        }

        public bool CreateStadion(StadionModel model)
        {
            var stadion = new Stadioane()
            {
                IdStadion = Guid.NewGuid(),
                Nume = model.Nume
            };

            UnitOfWork.Stadioane.Insert(stadion);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateStadion(StadionModel model)
        {
            var stadion = UnitOfWork.Stadioane.Find(model.IdStadion);
            if (stadion == null) return false;

            stadion.Nume = model.Nume;

            UnitOfWork.Stadioane.Update(stadion);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteStadion(Guid id)
        {
            var stadion = UnitOfWork.Stadioane.Find(id);
            if (stadion == null) return false;

            UnitOfWork.Stadioane.Delete(stadion);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
