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
using AJFIlfov.BusinessLogic.Implementation.Documente.Models;

namespace AJFIlfov.BusinessLogic.Implementation.Documente
{
    public class DocumenteService : BaseService
    {
        private readonly AuditService _auditService;

        public DocumenteService(ServiceDependencies dependencies, AuditService auditService)
            : base(dependencies)
        {
            this._auditService = auditService;
        }

        public List<DocumentModel> GetAllADocumente()
        {
            var anunturi = UnitOfWork.Documente.Get();
            return Mapper.Map<List<DocumentModel>>(anunturi);
        }

        public bool CreateDocument(DocumentModel anuntModel)
        {
            var anunt = Mapper.Map<Document>(anuntModel);

            UnitOfWork.Documente.Insert(anunt);
            _auditService.LogAction(CurrentUser.FirstName + " " + CurrentUser.LastName, "A adaugat un anunt nou!");
            UnitOfWork.SaveChanges();
            return true;
        }

    }
}