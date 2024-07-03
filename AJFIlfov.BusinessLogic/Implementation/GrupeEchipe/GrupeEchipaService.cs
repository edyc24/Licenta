using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService.Models;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.GrupeEchipaService
{
    public class GrupeEchipaService : BaseService
    {
        public GrupeEchipaService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<GrupaEchipaModel> GetAll()
        {
            var grupeEchipa = UnitOfWork.GrupeEchipa.Get()
                .Include(ge => ge.IdEchipaNavigation)  
                .Include(ge => ge.IdGrupaNavigation)   
                .ToList();

            var result = grupeEchipa.Select(ge => new GrupaEchipaModel
            {
                IdGrupaEchipa = ge.IdGrupaEchipa,
                IdEchipa = ge.IdEchipa,
                IdGrupa = ge.IdGrupa,
                EchipaNume = ge.IdEchipaNavigation != null ? ge.IdEchipaNavigation.Nume : "Unknown", 
                GrupaNume = ge.IdGrupaNavigation != null ? ge.IdGrupaNavigation.Nume : "Unknown"    
            }).ToList();


            return result;
        }

        public GrupaEchipaModel GetById(Guid id)
        {
            var grupaEchipa = UnitOfWork.GrupeEchipa.Find(id);
            if (grupaEchipa == null) return null;

            return new GrupaEchipaModel()
            {
                IdGrupaEchipa = grupaEchipa.IdGrupaEchipa,
                IdEchipa = grupaEchipa.IdEchipa,
                IdGrupa = grupaEchipa.IdGrupa,
                EchipaNume = grupaEchipa.IdEchipaNavigation?.Nume,
                GrupaNume = grupaEchipa.IdGrupaNavigation?.Nume
            };
        }

        public bool CreateGrupaEchipa(GrupaEchipaCreateModel model)
        {
            var grupaEchipa = new GrupeEchipa()
            {
                IdGrupaEchipa = Guid.NewGuid(),
                IdEchipa = model.IdEchipa,
                IdGrupa = model.IdGrupa
            };

            UnitOfWork.GrupeEchipa.Insert(grupaEchipa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateGrupaEchipa(GrupaEchipaModel model)
        {
            var grupaEchipa = UnitOfWork.GrupeEchipa.Find(model.IdGrupaEchipa);
            if (grupaEchipa == null) return false;

            grupaEchipa.IdEchipa = model.IdEchipa;
            grupaEchipa.IdGrupa = model.IdGrupa;

            UnitOfWork.GrupeEchipa.Update(grupaEchipa);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteGrupaEchipa(Guid id)
        {
            var grupaEchipa = UnitOfWork.GrupeEchipa.Find(id);
            if (grupaEchipa == null) return false;

            UnitOfWork.GrupeEchipa.Delete(grupaEchipa);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
