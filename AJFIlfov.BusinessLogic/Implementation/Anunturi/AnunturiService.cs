// BusinessLogic.Implementation.AnuntService/AnuntService.cs
using AJFIlfov.BusinessLogic.Implementation.Anunturi.Models;
using AJFIlfov.DataAccess;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.Common.DTOs;
using AJFIlfov.BusinessLogic.Implementation.Audituri;

namespace AJFIlfov.BusinessLogic.Implementation.Anunturi
{
    public class AnuntService : BaseService
    {
        private readonly AuditService _auditService;

        public AnuntService(ServiceDependencies dependencies,AuditService auditService) 
            : base(dependencies)
        {
            this._auditService = auditService;
        }

        public List<AnuntModel> GetAllAnunturi()
        {
            var anunturi = UnitOfWork.Anunturi.Get();
            return Mapper.Map<List<AnuntModel>>(anunturi);
        }

        public List<AnuntModel> GetAnunturiComisii()
        {
            return Mapper.Map<List<AnuntModel>>(UnitOfWork.Anunturi.Get()
                .Where(anunt => anunt.TipAnunt == "Comisia de disciplina" || anunt.TipAnunt == "Comisia pentru statul jucatorului" || anunt.TipAnunt == "Comisia de apel")
                .OrderByDescending(anunt => anunt.DataPublicarii)
                .ToList());
        }

        public bool CreateAnunt(AnuntModel anuntModel)
        {
            var anunt = Mapper.Map<Anunt>(anuntModel);
            anunt.DataPublicarii = DateTime.Now;
            anunt.PublishedBy = CurrentUser.FirstName + " " + CurrentUser.LastName;
            UnitOfWork.Anunturi.Insert(anunt);
            _auditService.LogAction(CurrentUser.FirstName + " " + CurrentUser.LastName , "A adaugat un anunt nou!");
            UnitOfWork.SaveChanges();
            return true;
        }

        public void UpdateAnunt(AnuntModel anuntModel)
        {
            // Retrieve the existing entity from the database
            var anunt = UnitOfWork.Anunturi.Get().Where(a => a.Id == anuntModel.Id).FirstOrDefault();
            if (anunt == null)
            {
                // Handle the case where the entity doesn't exist
                throw new Exception("Anunțul nu a fost găsit.");
            }

            // Update the properties
            anunt.Views = anuntModel.Views;
            // Update other properties as needed

            // Save changes
            UnitOfWork.SaveChanges();
        }


        public AnuntModel GetAnuntById(int id)
        {
            var anunt = UnitOfWork.Anunturi.Get().Where(a => a.Id == id).FirstOrDefault();
            return Mapper.Map<AnuntModel>(anunt);
        }
    }
}