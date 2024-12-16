// BusinessLogic.Implementation.AnuntService/AnuntService.cs
using AJFIlfov.BusinessLogic.Implementation.Audituri.Models;
using AJFIlfov.DataAccess;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.Common.DTOs;

namespace AJFIlfov.BusinessLogic.Implementation.Audituri
{
    public class AuditService : BaseService
    {

        public AuditService(ServiceDependencies dependencies) 
            : base(dependencies)
        {
        }

        public void LogAction(string username, string actionPerformed)
        {
            var log = new AuditModel
            {
                Username = username,
                ActionPerformed = actionPerformed,
                Timestamp = DateTime.Now
            };
            var logAudit = Mapper.Map<Audit>(log);

            UnitOfWork.Audits.Insert(logAudit);
            UnitOfWork.SaveChanges();
        }

        public List<AuditModel> GetAllLogs()
        {
            return (UnitOfWork.Audits.Get().Select(a => new AuditModel
            {
                Timestamp = a.Timestamp,
                Username = a.Username,
                ActionPerformed = a.ActionPerformed,
                Id = a.Id
            }).OrderByDescending(t => t.Timestamp).ToList());
        }
    }
}