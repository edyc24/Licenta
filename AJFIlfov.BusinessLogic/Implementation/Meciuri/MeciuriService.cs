using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.GrupeService.Models;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService.Models;
using AJFIlfov.Entities.Entities;
using AJFIlfov.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AJFIlfov.BusinessLogic.Implementation.MeciuriService
{
    public class MeciuriService : BaseService
    {
        private readonly SmsService _smsService;
        public MeciuriService(ServiceDependencies dependencies, SmsService smsService)
            : base(dependencies)
        {
            _smsService = smsService;
        }

        public List<MeciModel> GetAll()
        {
            var result = UnitOfWork.Meciuri.Get()
                .Include(m => m.IdEchipaGazdaNavigation)
                    .ThenInclude(ge => ge.IdGrupaNavigation)
                .Include(m => m.IdEchipaGazdaNavigation)
                    .ThenInclude(ge => ge.IdEchipaNavigation)
                .Include(m => m.IdEchipaOaspeteNavigation)
                    .ThenInclude(oe => oe.IdEchipaNavigation)
                .Include(m => m.IdArbitruNavigation)
                .Include(m => m.IdArbitruAsistent1Navigation)
                .Include(m => m.IdArbitruAsistent2Navigation)
                .Include(m => m.IdArbitruRezervaNavigation)
                .Include(m => m.IdObservatorNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdStadionNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdLocalitateNavigation)
                .ToList();

            var meciuri = result
                .Where(m => m.IdDeleted == false || m.IdDeleted == null)
                .Select(m => new MeciModel
                {
                    IdMeci = m.IdMeci,
                    IdEchipaGazda = m.IdEchipaGazda,
                    IdEchipaOaspete = m.IdEchipaOaspete,
                    DataJoc = m.DataJoc,
                    Rezultat = m.Rezultat,
                    Observatii = m.Observatii,
                    Raport = m.Raport,
                    IdArbitru = m.IdArbitru,
                    IdArbitruAsistent1 = m.IdArbitruAsistent1,
                    IdArbitruAsistent2 = m.IdArbitruAsistent2,
                    IdArbitruRezerva = m.IdArbitruRezerva,
                    IdObservator = m.IdObservator,
                    IdStadionLocalitate = m.IdStadionLocalitate,
                    EchipaGazdaNume = m.IdEchipaGazdaNavigation?.IdEchipaNavigation?.Nume,
                    GrupaNume = m.IdEchipaGazdaNavigation?.IdGrupaNavigation?.Nume,
                    EchipaOaspeteNume = m.IdEchipaOaspeteNavigation?.IdEchipaNavigation?.Nume,
                    ArbitruNume = m.IdArbitruNavigation?.Nume + ' ' + m.IdArbitruNavigation?.Prenume,
                    ArbitruAsistent1Nume = m.IdArbitruAsistent1Navigation?.Nume + ' ' + m.IdArbitruAsistent1Navigation?.Prenume,
                    ArbitruAsistent2Nume = m.IdArbitruAsistent2Navigation?.Nume + ' ' + m.IdArbitruAsistent2Navigation?.Prenume,
                    ArbitruRezervaNume = m.IdArbitruRezervaNavigation?.Nume + ' ' + m.IdArbitruRezervaNavigation?.Prenume,
                    ObservatorNume = m.IdObservatorNavigation?.Nume + ' ' + m.IdObservatorNavigation?.Prenume,
                    StadionNume = m.IdStadionLocalitateNavigation?.IdStadionNavigation?.Nume,
                    LocalitateNume = m.IdStadionLocalitateNavigation?.IdLocalitateNavigation?.Nume,
                    ScorGazde = m.ScorGazde,
                    ScorOaspeti = m.ScorOaspeti,
                    Locatie = m.Locatie,
                    Etapa = m.Etapa
                })
                .ToList();

            return meciuri;
        }


        public List<MeciModel> GetAllCurrentUser()
        {
            var result = GetAll().Where(m => m.IdArbitru == CurrentUser.Id ||
                            m.IdArbitruAsistent1 == CurrentUser.Id ||
                            m.IdArbitruAsistent2 == CurrentUser.Id ||
                            m.IdArbitruRezerva == CurrentUser.Id)
                .ToList();

            return result;
        }


        public MeciModel GetById(Guid id)
        {
            var meci = UnitOfWork.Meciuri
                .Get().Where(m => m.IdMeci == id)
                .Include(m => m.IdEchipaGazdaNavigation)
                    .ThenInclude(ge => ge.IdEchipaNavigation)
                .Include(m => m.IdEchipaOaspeteNavigation)
                    .ThenInclude(oe => oe.IdEchipaNavigation)
                .Include(m => m.IdArbitruNavigation)
                .Include(m => m.IdArbitruAsistent1Navigation)
                .Include(m => m.IdArbitruAsistent2Navigation)
                .Include(m => m.IdArbitruRezervaNavigation)
                .Include(m => m.IdObservatorNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdStadionNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdLocalitateNavigation)
                 .FirstOrDefault();
            if (meci == null) return null;

            return new MeciModel
            {
                IdMeci = meci.IdMeci,
                IdEchipaGazda = meci.IdEchipaGazda,
                IdEchipaOaspete = meci.IdEchipaOaspete,
                DataJoc = meci.DataJoc,
                Rezultat = meci.Rezultat,
                Observatii = meci.Observatii,
                Raport = meci.Raport,
                IdArbitru = meci.IdArbitru,
                IdArbitruAsistent1 = meci.IdArbitruAsistent1,
                IdArbitruAsistent2 = meci.IdArbitruAsistent2,
                IdArbitruRezerva = meci.IdArbitruRezerva,
                IdObservator = meci.IdObservator,
                IdStadionLocalitate = meci.IdStadionLocalitate,
                EchipaGazdaNume = meci.IdEchipaGazdaNavigation?.IdEchipaNavigation?.Nume,
                EchipaOaspeteNume = meci.IdEchipaOaspeteNavigation?.IdEchipaNavigation?.Nume,
                ScorGazde = meci.ScorGazde,
                ScorOaspeti = meci.ScorOaspeti,
                Locatie = meci.Locatie,
                Etapa = meci.Etapa
            };
        }

        public List<MeciAdminModel> GetMeciuriByGrupa(Guid id)
        {
            var meciuri = UnitOfWork.Meciuri
                .Get().Where(m => m.IdEchipaGazdaNavigation.IdGrupa == id && m.IdDeleted == false)
                .Include(m => m.IdEchipaGazdaNavigation)
                    .ThenInclude(ge => ge.IdEchipaNavigation)
                .Include(m => m.IdEchipaOaspeteNavigation)
                    .ThenInclude(oe => oe.IdEchipaNavigation)
                .Include(m => m.IdArbitruNavigation)
                .Include(m => m.IdArbitruAsistent1Navigation)
                .Include(m => m.IdArbitruAsistent2Navigation)
                .Include(m => m.IdArbitruRezervaNavigation)
                .Include(m => m.IdObservatorNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdStadionNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                    .ThenInclude(sl => sl.IdLocalitateNavigation)
                .ToList();

            if (meciuri == null || !meciuri.Any()) return null;

            return meciuri.Select(meci => new MeciAdminModel
            {
                IdMeci = meci.IdMeci,
                IdEchipaGazda = meci.IdEchipaGazda,
                IdEchipaOaspete = meci.IdEchipaOaspete,
                DataJoc = meci.DataJoc,
                IdArbitru = meci.IdArbitru,
                IdArbitruAsistent1 = meci.IdArbitruAsistent1,
                IdArbitruAsistent2 = meci.IdArbitruAsistent2,
                IdArbitruRezerva = meci.IdArbitruRezerva,
                IdObservator = meci.IdObservator,
                IdStadionLocalitate = meci.IdStadionLocalitate,
                EchipaGazdaNume = meci.IdEchipaGazdaNavigation?.IdEchipaNavigation?.Nume,
                EchipaOaspeteNume = meci.IdEchipaOaspeteNavigation?.IdEchipaNavigation?.Nume,
                ScorGazde = meci.ScorGazde,
                ScorOaspeti = meci.ScorOaspeti,
                Locatie = meci.Locatie,
                Etapa = meci.Etapa
            }).ToList();
        }


        public bool CreateMeci(MeciAdminModel model)
        {
            var idEchipaGazda = UnitOfWork.GrupeEchipa.Get().Where(e => e.IdGrupa == model.IdGrupa && e.IdEchipa == model.IdEchipaGazda).First().IdGrupaEchipa;
            var idEchipaOaspete = UnitOfWork.GrupeEchipa.Get().Where(e => e.IdGrupa == model.IdGrupa && e.IdEchipa == model.IdEchipaOaspete).First().IdGrupaEchipa;
            var meci = new Meciuri
            {
                IdMeci = Guid.NewGuid(),
                IdEchipaGazda = idEchipaGazda,
                IdEchipaOaspete = idEchipaOaspete,
                DataJoc = model.DataJoc,
                IdArbitru = model.IdArbitru,
                IdArbitruAsistent1 = model.IdArbitruAsistent1,
                IdArbitruAsistent2 = model.IdArbitruAsistent2,
                IdArbitruRezerva = model.IdArbitruRezerva,
                IdObservator = model.IdObservator,
               
                IdStadionLocalitate = model.IdStadionLocalitate,
                IdDeleted = false,
                Locatie = model.Locatie,
                Etapa = model.Etapa
            };

            UnitOfWork.Meciuri.Insert(meci);
            UnitOfWork.SaveChanges();

            // Send SMS to all assigned referees
            NotifyReferees(meci);
            return true;
        }

        public List<int> GetEtapeByGrupa(Guid grupaId)
        {
            // Preia lista de etape distincte
            return UnitOfWork.Meciuri.Get()
                .Where(m => m.IdEchipaGazdaNavigation.IdGrupa == grupaId && m.IdDeleted == false)
                .Select(m => m.Etapa.Value)
                .Distinct()
                .OrderBy(e => e)
                .ToList();
        }


        private void NotifyReferees(Meciuri meci)
        {
            var arbitri = new List<Guid?>
            {
                meci.IdArbitru,
                meci.IdArbitruAsistent1,
                meci.IdArbitruAsistent2,
                meci.IdArbitruRezerva,
                meci.IdObservator
            };
            var localitateStadion = (StadionNume: (string)null, LocalitateNume: (string)null);

            if (meci.IdStadionLocalitate != null)
            {
                var result = UnitOfWork.StadionLocalitate.Get()
                    .Include(sl => sl.IdStadionNavigation) // Include the Stadioane entity
                    .Include(sl => sl.IdLocalitateNavigation) // Include the Localitati entity
                    .Where(sl => sl.IdStadionLocalitate == meci.IdStadionLocalitate)
                    .Select(sl => new
                    {
                        StadionNume = sl.IdStadionNavigation.Nume, // Access the Clinceni
                        LocalitateNume = sl.IdLocalitateNavigation.Nume // Access the locality name
                    })
                    .FirstOrDefault(); // Use FirstOrDefault to get the single result or null if not found

                // If result is not null, update the tuple
                if (result != null)
                {
                    localitateStadion = (result.StadionNume, result.LocalitateNume);
                }
            }

            // Initialize as nullable tuples to hold team names
            var echipaGazda = String.Empty;
            var echipaOaspete = String.Empty;

            if (meci.IdEchipaGazda != null)
            {
                var gazdaResult = UnitOfWork.GrupeEchipa.Get()
                    .Include(ge => ge.IdEchipaNavigation) // Include the Echipe entity
                    .Where(ge => ge.IdGrupaEchipa == meci.IdEchipaGazda)
                    .Select(ge => new
                    {
                        EchipaNume = ge.IdEchipaNavigation.Nume // Access the team name
                    })
                    .FirstOrDefault(); // Use FirstOrDefault to get the single result or null if not found

                if (gazdaResult != null)
                {
                    echipaGazda = gazdaResult.EchipaNume;
                }
            }

            if (meci.IdEchipaOaspete != null)
            {
                var oaspeteResult = UnitOfWork.GrupeEchipa.Get()
                    .Include(ge => ge.IdEchipaNavigation) // Include the Echipe entity
                    .Where(ge => ge.IdGrupaEchipa == meci.IdEchipaOaspete)
                    .Select(ge => new
                    {
                        EchipaNume = ge.IdEchipaNavigation.Nume // Access the team name
                    })
                    .FirstOrDefault(); // Use FirstOrDefault to get the single result or null if not found

                if (oaspeteResult != null)
                {
                    echipaOaspete = oaspeteResult.EchipaNume;
                }
            }



            var referees = UnitOfWork.Users.Get().Where(u => arbitri.Contains(u.IdUtilizator)).ToList();

            foreach (var referee in referees)
            {
                if (referee != null && !string.IsNullOrWhiteSpace(referee.NumarTelefon))
                {
                    var message = $"Ai fost delegat la un meci!\n" +
                                  $"Data: {meci.DataJoc?.ToString("dd MMM yyyy")}\n" +
                                  $"Location: {localitateStadion.LocalitateNume} , {localitateStadion.StadionNume}\n" +
                                  $"Pentru mai multe detalii acceseaza site-ul!";

                    _smsService.SendSms(referee.NumarTelefon, message);
                }
            }
        }

        public bool UpdateMeciuriByAdmin(List<MeciAdminModel> models)
        {
            foreach (var model in models)
            {
                var meci = UnitOfWork.Meciuri.Get().AsNoTracking().FirstOrDefault(m => m.IdMeci == model.IdMeci);
                if (meci == null) continue;

                var meciEditat = new Meciuri
                {
                    IdMeci = meci.IdMeci,
                    IdEchipaGazda = meci.IdEchipaGazda,
                    IdEchipaOaspete = meci.IdEchipaOaspete,
                    DataJoc = meci.DataJoc,
                    IdArbitru = model.IdArbitru,
                    IdArbitruAsistent1 = model.IdArbitruAsistent1,
                    IdArbitruAsistent2 = model.IdArbitruAsistent2,
                    IdArbitruRezerva = model.IdArbitruRezerva,
                    IdObservator = model.IdObservator,
                    IdStadionLocalitate = meci.IdStadionLocalitate
                };

                UnitOfWork.Meciuri.Update(meciEditat);
            }

            UnitOfWork.SaveChanges();

            // Send SMS to all assigned referees for each updated match
            foreach (var model in models)
            {
                var meci = UnitOfWork.Meciuri.Get().AsNoTracking().FirstOrDefault(m => m.IdMeci == model.IdMeci);
                if (meci == null) continue;

                var meciEditat = new Meciuri
                {
                    IdMeci = meci.IdMeci,
                    IdEchipaGazda = meci.IdEchipaGazda,
                    IdEchipaOaspete = meci.IdEchipaOaspete,
                    DataJoc = meci.DataJoc,
                    IdArbitru = model.IdArbitru,
                    IdArbitruAsistent1 = model.IdArbitruAsistent1,
                    IdArbitruAsistent2 = model.IdArbitruAsistent2,
                    IdArbitruRezerva = model.IdArbitruRezerva,
                    IdObservator = model.IdObservator,
                    IdStadionLocalitate = meci.IdStadionLocalitate
                };

                NotifyReferees(meciEditat);
            }

            return true;
        }

        public string GetRefAddress(Guid id)
        {
            var user = UnitOfWork.UserAddresses.Find(id);
            if (user != null)
            {
                return GetFullAddress(user);
            }
            else
            {
                return null;
            }
        }

        public string GetStadiumAddress(Guid id)
        {
            var stadium = UnitOfWork.StadionLocalitate.Get().Where(s => s.IdStadionLocalitate == id).Include(s => s.IdLocalitateNavigation).FirstOrDefault();
           
                return stadium.IdLocalitateNavigation.Address;
        }
        public string GetFullAddress(UserAddress referee)
        {
            var components = new List<string>
            {
                referee.StreetAddress,
                referee.City,
                referee.State,
                referee.ZipCode,
                referee.Country
            };

            var fullAddress = string.Join(", ", components.Where(c => !string.IsNullOrWhiteSpace(c)));
            return fullAddress;
        }

        public bool UpdateMeciByArbitru(MeciArbitruModel model, byte[] raport)
        {
            var meci = UnitOfWork.Meciuri.Find(model.IdMeci);
            if (meci == null) return false;

            meci.Rezultat = model.Rezultat;
            meci.Observatii = model.Observatii;
            meci.Raport = raport;

            UnitOfWork.Meciuri.Update(meci);
            UnitOfWork.SaveChanges();
            return true;
        }
        public bool UpdateMeciByAdmin(MeciAdminModel model)
        {
            var meci = UnitOfWork.Meciuri.Get().AsNoTracking().FirstOrDefault(m => m.IdMeci == model.IdMeci);
            if (meci == null) return false;

            var meciEditat = new Meciuri
            {
                IdMeci = model.IdMeci,
                IdEchipaGazda = model.IdEchipaGazda,
                IdEchipaOaspete = model.IdEchipaOaspete,
                DataJoc = model.DataJoc,
                IdArbitru = model.IdArbitru,
                IdArbitruAsistent1 = model.IdArbitruAsistent1,
                IdArbitruAsistent2 = model.IdArbitruAsistent2,
                IdArbitruRezerva = model.IdArbitruRezerva,
                IdObservator = model.IdObservator,
                IdStadionLocalitate = model.IdStadionLocalitate
            };

            UnitOfWork.Meciuri.Update(meciEditat);
            UnitOfWork.SaveChanges();

            // Send SMS to all assigned referees
            NotifyReferees(meciEditat);
            return true;
        }
        public MeciAdminModel GetMeciAdminById(Guid id)
        {
            var meci = UnitOfWork.Meciuri.Get()
                .Include(m => m.IdEchipaGazdaNavigation)
                .Include(m => m.IdEchipaOaspeteNavigation)
                .Include(m => m.IdStadionLocalitateNavigation)
                .FirstOrDefault(m => m.IdMeci == id);

            return Mapper.Map<Meciuri, MeciAdminModel>(meci);
        }

        public bool DeleteMeci(Guid id)
        {
            var meci = UnitOfWork.Meciuri.Find(id);
            if (meci == null) return false;

            // Mark as deleted instead of physical deletion
            meci.IdDeleted = true;

            UnitOfWork.Meciuri.Update(meci);
            UnitOfWork.SaveChanges();
            return true;
        }

        // Methods to get data for dropdown lists
        public List<TeamModel> GetAllTeams()
        {
            return UnitOfWork.GrupeEchipa.Get()
                .Select(ge => new TeamModel
                {
                    Id = ge.IdGrupaEchipa,
                    Nume = ge.IdEchipaNavigation.Nume
                }).ToList();
        }

        public List<RefereeModel> GetAllArbitri(DateTime? data)
        {
            if (data == null)
            {
                return new List<RefereeModel>();
            }

            // Calculate the required end time (match time + 2 hours)
            var requiredEndTime = data.Value.AddHours(2);

            return UnitOfWork.Users.Get()
                .Where(u => (u.IdRol == 3 || u.IdRol == 5) &&
                    (
                        // Select referees who have no availability record for the given date
                        !u.Disponibilitates.Any(d => d.Zi == data.Value.Date) ||

                        // Select referees who are partially available on the given date
                        u.Disponibilitates.Any(d => d.Zi == data.Value.Date && d.Status == "DisponibilPartial" && d.OraIncheiere >= requiredEndTime)
                    ))
                .Include(u => u.IdCategorieNavigation)
                .Include(u => u.IdRolNavigation)
                .Select(u => new RefereeModel
                {
                    Id = u.IdUtilizator,
                    Nume = u.Nume + " " + u.Prenume + " - " + u.IdRolNavigation.Nume + " - " + u.IdCategorieNavigation.Categorie
                }).ToList();
        }

        public List<RefereeModel> GetAllArbitriAll()
        {
            return UnitOfWork.Users.Get()
                .Where(u => (u.IdRol == 3 || u.IdRol == 5))
                .Include(u => u.IdCategorieNavigation)
                .Include(u => u.IdRolNavigation)
                .Select(u => new RefereeModel
                {
                    Id = u.IdUtilizator,
                    Nume = u.Nume + " " + u.Prenume + " - " + u.IdRolNavigation.Nume + " - " + u.IdCategorieNavigation.Categorie
                }).ToList();
        }

        public List<RefereeModel> GetAllObservatori()
        {
            return UnitOfWork.Users.Get()
                .Where(u => u.IdRol == 4)
                .Select(u => new RefereeModel
                {
                    Id = u.IdUtilizator,
                    Nume = u.Nume + ' ' + u.Prenume + '-' + u.IdRolNavigation.Nume + '-' + u.IdCategorieNavigation.Categorie
                }).ToList();
        }

        public List<StadiumModel> GetAllStadioane()
        {
            return UnitOfWork.StadionLocalitate.Get()
                .Select(sl => new StadiumModel
                {
                    Id = sl.IdStadionLocalitate,
                    Nume = sl.IdStadionNavigation.Nume
                }).ToList();
        }
        public List<GrupaModel> GetAllGrupe()
        {
            return UnitOfWork.Grupe.Get()
                .Select(g => new GrupaModel
                {
                    IdGrupa = g.IdGrupa,
                    Nume = g.Nume
                }).ToList();
        }

        public List<TeamModel> GetTeamsByGrupa(Guid grupaId)
        {
            return UnitOfWork.GrupeEchipa.Get()
                .Where(ge => ge.IdGrupa == grupaId)
                .Select(ge => new TeamModel
                {
                    Id = ge.IdEchipa,
                    Nume = ge.IdEchipaNavigation.Nume
                }).ToList();
        }

    }

    // Models for dropdowns
    public class TeamModel
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
    }

    public class RefereeModel
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
    }

    public class StadiumModel
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
    }
}
