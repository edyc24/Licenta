using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService.Models;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.StadionLocalitateService
{
    public class StadionLocalitateService : BaseService
    {
        public StadionLocalitateService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<StadionLocalitateModel> GetAll()
        {
            var stadionLocalitate = UnitOfWork.StadionLocalitate.Get()
                .Include(ge => ge.IdStadionNavigation)  
                .Include(ge => ge.IdLocalitateNavigation)   
                .ToList(); 

            var result = stadionLocalitate
                .Select(sl => new StadionLocalitateModel()
                {
                    IdStadionLocalitate = sl.IdStadionLocalitate,
                    IdStadion = sl.IdStadion,
                    IdLocalitate = sl.IdLocalitate,
                    StadionNume = sl.IdStadionNavigation?.Nume,
                    LocalitateNume = sl.IdLocalitateNavigation?.Nume
                }).ToList();

            return result;
        }

        public StadionLocalitateModel GetById(Guid id)
        {
            var sl = UnitOfWork.StadionLocalitate.Find(id);
            if (sl == null) return null;

            return new StadionLocalitateModel()
            {
                IdStadionLocalitate = sl.IdStadionLocalitate,
                IdStadion = sl.IdStadion,
                IdLocalitate = sl.IdLocalitate,
                StadionNume = sl.IdStadionNavigation?.Nume,
                LocalitateNume = sl.IdLocalitateNavigation?.Nume
            };
        }

        public bool CreateStadionLocalitate(StadionLocalitateCreateModel model)
        {
            var stadionLocalitate = new StadionLocalitate()
            {
                IdStadionLocalitate = Guid.NewGuid(),
                IdStadion = model.IdStadion,
                IdLocalitate = model.IdLocalitate
            };

            UnitOfWork.StadionLocalitate.Insert(stadionLocalitate);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateStadionLocalitate(StadionLocalitateModel model)
        {
            var stadionLocalitate = UnitOfWork.StadionLocalitate.Find(model.IdStadionLocalitate);
            if (stadionLocalitate == null) return false;

            stadionLocalitate.IdStadion = model.IdStadion;
            stadionLocalitate.IdLocalitate = model.IdLocalitate;

            UnitOfWork.StadionLocalitate.Update(stadionLocalitate);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteStadionLocalitate(Guid id)
        {
            var stadionLocalitate = UnitOfWork.StadionLocalitate.Find(id);
            if (stadionLocalitate == null) return false;

            UnitOfWork.StadionLocalitate.Delete(stadionLocalitate);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
