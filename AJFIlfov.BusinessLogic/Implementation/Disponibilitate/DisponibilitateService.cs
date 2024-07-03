using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Account;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateService.Models;
using AJFIlfov.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateService
{
    public class DisponibilitateService : BaseService
    {
        private readonly MailService _mailService;
        public DisponibilitateService(ServiceDependencies dependencies, MailService mailService)
            : base(dependencies)
        {
            _mailService = mailService;
        }

        public List<CerereModel> GetCereri()
        {
            //to check if current user status is active
            var cereri = UnitOfWork.DisponibilitateAdmin.Get().Where(d => d.CreatedOn > DateTime.Now.AddHours(-48)).Select(d => new CerereModel()
            {
                IdCerere = d.IdDisponibilitateAdmin,
                Zi = d.Zi,
                CreatedOn = d.CreatedOn
            }).ToList();
            foreach(var cerere in cereri)
            {
                var disponibilitate = UnitOfWork.Disponibilitate.Get().Where(d => d.Zi == cerere.Zi).FirstOrDefault();
                if(disponibilitate!= null)
                {
                    cerere.Status = disponibilitate.Status;
                    cerere.OraIncheiere = disponibilitate.OraIncheiere;
                    cerere.OraIncepere = disponibilitate.OraIncepere;
                }
            }
            return cereri;
        }
        public bool Adauga(CerereModel cerereModel)
        {
            var disponibilitate = Mapper.Map<CerereModel, Disponibilitate>(cerereModel);
            disponibilitate.IdUtilizator = CurrentUser.Id;
            var existing = UnitOfWork.Disponibilitate.Get().Where(d => d.Zi == cerereModel.Zi && d.IdUtilizator == disponibilitate.IdUtilizator).FirstOrDefault();
            if(existing == null)
            {
                UnitOfWork.Disponibilitate.Insert(disponibilitate);
            }
            else
            {
                existing.Status = disponibilitate.Status;
                existing.OraIncepere = disponibilitate.OraIncepere;
                existing.OraIncheiere = disponibilitate.OraIncheiere;
            }
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
