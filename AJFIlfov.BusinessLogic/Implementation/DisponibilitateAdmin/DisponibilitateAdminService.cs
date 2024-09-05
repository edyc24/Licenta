using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService.Models;
using AJFIlfov.DataAccess;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJFIlfov.BusinessLogic.Implementation.DisponibilitateAdminService
{
    public class DisponibilitateAdminService : BaseService
    {
        private readonly MailService _mailService;
        public DisponibilitateAdminService(ServiceDependencies dependencies, MailService mailService)
            : base(dependencies)
        {
            _mailService = mailService;
        }

        public List<CerereDisponibilitateModel> GetAllAvailable()
        {
            var cereri = UnitOfWork.DisponibilitateAdmin.Get().Where(d => d.CreatedOn > DateTime.Now.AddHours(-48)).Select(d => new CerereDisponibilitateModel()
            {
                IdDisponibilitateAdmin = d.IdDisponibilitateAdmin,
                CreatedOn = d.CreatedOn,
                Zi = d.Zi
            }).ToList();
            return cereri;
        }

        public bool CreateDisponibilitate(CerereDisponibilitateModel cerere) 
        {
            var cerereAdmin = Mapper.Map<CerereDisponibilitateModel, DisponibilitateAdmin>(cerere);
            UnitOfWork.DisponibilitateAdmin.Insert(cerereAdmin);
            UnitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<RefereeAvailabilityModel> GetRefereesAvailabilityByDay(DateTime zi)
        {
            // Fetch all referees
            var referees = UnitOfWork.Users.Get()
                .Where(u => u.IdRol == 3 || u.IdRol == 5)
                .Include(m => m.IdRolNavigation)
                .Include(m => m.IdCategorieNavigation)
                .ToList();

            // Fetch all availability records for the specific day
            var availabilities = UnitOfWork.Disponibilitate.Get()
                                 .Where(a => a.Zi == zi)
                                 .ToList();

            // Map referees and their availability status for the specific day
            var refereeAvailabilityList = referees.Select(referee => new RefereeAvailabilityModel
            {
                RefereeName = referee.Nume,
                Rol = referee.IdRolNavigation.Nume,
                Categorie = referee.IdCategorieNavigation.Categorie,
                Availability = availabilities.Where(a => a.IdUtilizator == referee.IdUtilizator)
                                             .Select(a => new AvailabilityStatus
                                             {
                                                 Zi = a.Zi.Value.ToString("yyyy-MM-dd"),
                                                 Status = a.Status ?? "Indisponibil",
                                                 OraIncepere = a.OraIncepere.HasValue ? a.OraIncepere.Value.ToString("HH:mm") : null,
                                                 OraIncheiere = a.OraIncheiere.HasValue ? a.OraIncheiere.Value.ToString("HH:mm") : null
                                             }).ToList()
            });

            return refereeAvailabilityList;
        }

    }
}
