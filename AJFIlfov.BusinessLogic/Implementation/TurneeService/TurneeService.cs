using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.GrupeService.Models;
using AJFIlfov.BusinessLogic.Implementation.MeciuriService;
using AJFIlfov.BusinessLogic.Implementation.TurneeService.Models;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AJFIlfov.BusinessLogic.Implementation.TurneeService
{
    public class TurneeService : BaseService
    {
        public TurneeService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public List<TurneuModel> GetAll()
        {
            return UnitOfWork.Turnee.Get()
                .Include(t => t.IdEchipaGazdaNavigation)
                .Include(t => t.IdEchipaOaspeteNavigation)
                .Include(t => t.IdStadionNavigation)
                .Include(t => t.IdCategorieNavigation)
                .Include(t => t.IdGrupaNavigation)
                .Where(t => t.IdDeleted == false || t.IdDeleted == null)
                .Select(t => new TurneuModel
                {
                    IdTurneu = t.IdTurneu,
                    Data = t.Data,
                    IdEchipaGazda = t.IdEchipaGazda,
                    IdEchipaOaspete = t.IdEchipaOaspete,
                    IdStadion = t.IdStadion,
                    IdCategorie = t.IdCategorie,
                    IdGrupa = t.IdGrupa,
                    ScorGazda = t.ScorGazda,
                    ScorOaspeti = t.ScorOaspeti,
                    EchipaGazdaNume = t.IdEchipaGazdaNavigation.Nume,
                    EchipaOaspeteNume = t.IdEchipaOaspeteNavigation.Nume,
                    StadionNume = t.IdStadionNavigation.Nume,
                    CategorieNume = t.IdCategorieNavigation.Nume,
                    GrupaNume = t.IdGrupaNavigation.Nume
                })
                .ToList();
        }

        public List<TurneuModel> GetMeciuriByCategorie(string categorie)
        {
            return UnitOfWork.Turnee.Get()
                .Include(t => t.IdEchipaGazdaNavigation)
                .Include(t => t.IdEchipaOaspeteNavigation)
                .Include(t => t.IdStadionNavigation)
                .Include(t => t.IdCategorieNavigation)
                .Include(t => t.IdGrupaNavigation)
                .Where(t => t.IdDeleted == false && t.IdCategorieNavigation.Nume == categorie)
                .Select(t => new TurneuModel
                {
                    IdTurneu = t.IdTurneu,
                    Data = t.Data,
                    IdEchipaGazda = t.IdEchipaGazda,
                    IdEchipaOaspete = t.IdEchipaOaspete,
                    IdStadion = t.IdStadion,
                    IdCategorie = t.IdCategorie,
                    IdGrupa = t.IdGrupa,
                    ScorGazda = t.ScorGazda,
                    ScorOaspeti = t.ScorOaspeti,
                    EchipaGazdaNume = t.IdEchipaGazdaNavigation.Nume,
                    EchipaOaspeteNume = t.IdEchipaOaspeteNavigation.Nume,
                    StadionNume = t.IdStadionNavigation.Nume,
                    CategorieNume = t.IdCategorieNavigation.Nume,
                    GrupaNume = t.IdGrupaNavigation.Nume
                })
                .ToList();
        }

        public Dictionary<string, List<GroupStanding>> CalculateGroupStandings(List<TurneuModel> meciuri)
        {
            var grupe = new Dictionary<string, List<GroupStanding>>();

            foreach (var meci in meciuri)
            {
                if (!grupe.ContainsKey(meci.GrupaNume))
                {
                    grupe[meci.GrupaNume] = new List<GroupStanding>();
                }

                var grupa = grupe[meci.GrupaNume];
                UpdateTeamStanding(grupa, meci.EchipaGazdaNume, meci.ScorGazda, meci.ScorOaspeti);
                UpdateTeamStanding(grupa, meci.EchipaOaspeteNume, meci.ScorOaspeti, meci.ScorGazda);
            }

            // Sort each group by points, goal difference, and goals scored
            foreach (var grupa in grupe.Values)
            {
                grupa.Sort((a, b) =>
                {
                    if (a.Points != b.Points) return b.Points.CompareTo(a.Points);
                    if (a.GoalDifference != b.GoalDifference) return b.GoalDifference.CompareTo(a.GoalDifference);
                    return b.GoalsScored.CompareTo(a.GoalsScored);
                });
            }

            return grupe;
        }

        private void UpdateTeamStanding(List<GroupStanding> grupa, string teamName, int? goalsFor, int? goalsAgainst)
        {
            var standing = grupa.FirstOrDefault(s => s.TeamName == teamName);
            if (standing == null)
            {
                standing = new GroupStanding { TeamName = teamName };
                grupa.Add(standing);
            }


            standing.GoalsScored += goalsFor ?? 0;
            standing.GoalsConceded += goalsAgainst ?? 0;
            standing.GoalDifference = standing.GoalsScored - standing.GoalsConceded;

            if (goalsFor != null && goalsAgainst != null)
            {
                standing.MatchesPlayed++;

                if (goalsFor > goalsAgainst)
                {
                    standing.Wins++;
                    standing.Points += 3;
                }
                else if (goalsFor == goalsAgainst)
                {
                    standing.Draws++;
                    standing.Points += 1;
                }
                else
                {
                    standing.Losses++;
                }
            }
        }

        public List<TurneuModel> GetUpcomingMatches(string categorie)
        {
            var now = DateTime.UtcNow;
            return UnitOfWork.Turnee.Get()
                .Include(t => t.IdEchipaGazdaNavigation)
                .Include(t => t.IdEchipaOaspeteNavigation)
                .Include(t => t.IdStadionNavigation)
                .Include(t => t.IdCategorieNavigation)
                .Include(t => t.IdGrupaNavigation)
                .Where(t => t.IdDeleted == false &&
                           t.IdCategorieNavigation.Nume == categorie)
                .Select(t => new TurneuModel
                {
                    IdTurneu = t.IdTurneu,
                    Data = t.Data,
                    IdEchipaGazda = t.IdEchipaGazda,
                    IdEchipaOaspete = t.IdEchipaOaspete,
                    IdStadion = t.IdStadion,
                    IdCategorie = t.IdCategorie,
                    IdGrupa = t.IdGrupa,
                    ScorGazda = t.ScorGazda,
                    ScorOaspeti = t.ScorOaspeti,
                    EchipaGazdaNume = t.IdEchipaGazdaNavigation.Nume,
                    EchipaOaspeteNume = t.IdEchipaOaspeteNavigation.Nume,
                    StadionNume = t.IdStadionNavigation.Nume,
                    CategorieNume = t.IdCategorieNavigation.Nume,
                    GrupaNume = t.IdGrupaNavigation.Nume
                })
                .OrderBy(t => t.Data)
                .ToList();
        }

        public List<TurneuModel> GetEliminationStageMatches(List<TurneuModel> meciuri)
        {
            // Get all matches that are in elimination stage (no group assigned)
            return meciuri
                .Where(m => m.IdGrupa == null)
                .OrderBy(m => m.Data)
                .ToList();
        }

        public TurneuModel GetById(Guid id)
        {
            var turneu = UnitOfWork.Turnee.Get()
                .Include(t => t.IdEchipaGazdaNavigation)
                .Include(t => t.IdEchipaOaspeteNavigation)
                .Include(t => t.IdStadionNavigation)
                .Include(t => t.IdCategorieNavigation)
                .Include(t => t.IdGrupaNavigation)
                .FirstOrDefault(t => t.IdTurneu == id && (t.IdDeleted == false || t.IdDeleted == null));

            if (turneu == null) return null;

            return new TurneuModel
            {
                IdTurneu = turneu.IdTurneu,
                Data = turneu.Data,
                IdEchipaGazda = turneu.IdEchipaGazda,
                IdEchipaOaspete = turneu.IdEchipaOaspete,
                IdStadion = turneu.IdStadion,
                IdCategorie = turneu.IdCategorie,
                IdGrupa = turneu.IdGrupa,
                ScorGazda = turneu.ScorGazda,
                ScorOaspeti = turneu.ScorOaspeti,
                EchipaGazdaNume = turneu.IdEchipaGazdaNavigation.Nume,
                EchipaOaspeteNume = turneu.IdEchipaOaspeteNavigation.Nume,
                StadionNume = turneu.IdStadionNavigation.Nume,
                CategorieNume = turneu.IdCategorieNavigation.Nume,
                GrupaNume = turneu.IdGrupaNavigation.Nume
            };
        }

        public bool Create(TurneuModel model)
        {
            var turneu = new Turnee
            {
                IdTurneu = Guid.NewGuid(),
                Data = model.Data,
                IdEchipaGazda = model.IdEchipaGazda,
                IdEchipaOaspete = model.IdEchipaOaspete,
                IdStadion = model.IdStadion,
                IdCategorie = model.IdCategorie,
                IdGrupa = model.IdGrupa,
                ScorGazda = model.ScorGazda,
                ScorOaspeti = model.ScorOaspeti,
                IdDeleted = false
            };

            UnitOfWork.Turnee.Insert(turneu);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Update(TurneuModel model)
        {
            var turneu = UnitOfWork.Turnee.Get()
                .FirstOrDefault(t => t.IdTurneu == model.IdTurneu && (t.IdDeleted == false || t.IdDeleted == null));

            if (turneu == null) return false;

            turneu.Data = model.Data;
            turneu.IdEchipaGazda = model.IdEchipaGazda;
            turneu.IdEchipaOaspete = model.IdEchipaOaspete;
            turneu.IdStadion = model.IdStadion;
            turneu.IdCategorie = model.IdCategorie;
            turneu.IdGrupa = model.IdGrupa;
            turneu.ScorGazda = model.ScorGazda;
            turneu.ScorOaspeti = model.ScorOaspeti;

            UnitOfWork.Turnee.Update(turneu);
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var turneu = UnitOfWork.Turnee.Get()
                .FirstOrDefault(t => t.IdTurneu == id && (t.IdDeleted == false || t.IdDeleted == null));

            if (turneu == null) return false;

            turneu.IdDeleted = true;
            UnitOfWork.Turnee.Update(turneu);
            UnitOfWork.SaveChanges();
            return true;
        }

        public List<TeamModel> GetEchipeForDropdown()
        {
            return UnitOfWork.Echipe.Get()
                .Select(e => new TeamModel
                {
                    Id = e.IdEchipa,
                    Nume = e.Nume
                })
                .ToList();
        }

        public List<StadiumModel> GetStadioaneForDropdown()
        {
            return UnitOfWork.Stadioane.Get()
                .Select(s => new StadiumModel
                {
                    Id = s.IdStadion,
                    Nume = s.Nume
                })
                .ToList();
        }

        public List<RefereeModel> GetCategoriiForDropdown()
        {
            return UnitOfWork.CategoriiTurneu.Get()
                .Where(c => c.IdDeleted == false || c.IdDeleted == null)
                .Select(c => new RefereeModel
                {
                    Id = c.IdCategorie,
                    Nume = c.Nume
                })
                .ToList();
        }

        public List<GrupaModel> GetGrupeForDropdown()
        {
            return UnitOfWork.Grupe.Get()
                .Select(g => new GrupaModel
                {
                    IdGrupa = g.IdGrupa,
                    Nume = g.Nume
                })
                .ToList();
        }

        public bool CreateMatch(CreateTurneuModel model)
        {
            var turneu = new Turnee
            {
                IdTurneu = Guid.NewGuid(),
                Data = model.Data,
                IdEchipaGazda = model.IdEchipaGazda,
                IdEchipaOaspete = model.IdEchipaOaspete,
                IdStadion = model.IdStadion,
                IdCategorie = model.IdCategorie,
                IdGrupa = model.IdGrupa,
                ScorGazda = model.ScorGazda,
                ScorOaspeti = model.ScorOaspeti,
                IdDeleted = false
            };

            UnitOfWork.Turnee.Insert(turneu);
            UnitOfWork.SaveChanges();
            return true;
        }

        public Guid CreateMatch2(CreateTurneuModel model)
        {
            var x = Guid.NewGuid();

            var turneu = new Turnee
            {
                IdTurneu = x,
                Data = model.Data,
                IdEchipaGazda = model.IdEchipaGazda,
                IdEchipaOaspete = model.IdEchipaOaspete,
                IdStadion = model.IdStadion,
                IdCategorie = model.IdCategorie,
                IdGrupa = Guid.Parse("d9c7cbe8-4c79-48c1-aefc-bf2047789f07"),
                ScorGazda = model.ScorGazda,
                ScorOaspeti = model.ScorOaspeti,
                IdDeleted = false
            };

            UnitOfWork.Turnee.Insert(turneu);
            UnitOfWork.SaveChanges();
            return x;
        }

        public bool UpdateScore(Guid matchId, int homeScore, int awayScore)
        {
            var turneu = UnitOfWork.Turnee.Get()
                .FirstOrDefault(t => t.IdTurneu == matchId && (t.IdDeleted == false || t.IdDeleted == null));

            if (turneu == null) return false;

            turneu.ScorGazda = homeScore;
            turneu.ScorOaspeti = awayScore;

            UnitOfWork.Turnee.Update(turneu);
            UnitOfWork.SaveChanges();
            return true;
        }
    }

    public class GroupStanding
    {
        public string TeamName { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
    }
}