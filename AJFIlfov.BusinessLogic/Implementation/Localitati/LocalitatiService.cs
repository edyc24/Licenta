using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.LocalitatiService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.LocalitatiService
{
    public class LocalitatiService : BaseService
    {
        public LocalitatiService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<LocalitateModel> GetAll()
        {
            var localitati = UnitOfWork.Localitati.Get().Select(l => new LocalitateModel()
            {
                IdLocalitate = l.IdLocalitate,
                Nume = l.Nume
            }).ToList();

            return localitati;
        }

        public LocalitateModel GetById(Guid id)
        {
            var localitate = UnitOfWork.Localitati.Find(id);
            if (localitate == null) return null;

            return new LocalitateModel()
            {
                IdLocalitate = localitate.IdLocalitate,
                Nume = localitate.Nume
            };
        }

        public bool CreateLocalitate(LocalitateModel model)
        {
            var localitate = new Localitati()
            {
                IdLocalitate = Guid.NewGuid(),
                Nume = model.Nume
            };

            UnitOfWork.Localitati.Insert(localitate);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateLocalitate(LocalitateModel model)
        {
            var localitate = UnitOfWork.Localitati.Find(model.IdLocalitate);
            if (localitate == null) return false;

            localitate.Nume = model.Nume;

            UnitOfWork.Localitati.Update(localitate);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteLocalitate(Guid id)
        {
            var localitate = UnitOfWork.Localitati.Find(id);
            if (localitate == null) return false;

            UnitOfWork.Localitati.Delete(localitate);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
