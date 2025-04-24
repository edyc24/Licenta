using AJFIlfov.BusinessLogic.Implementation.TurneeService;
using AJFIlfov.BusinessLogic.Implementation.TurneeService.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AJFIlfovWebsite.Controllers
{
    public class TurneeController : BaseController
    {
        private readonly TurneeService _service;

        public TurneeController(ControllerDependencies dependencies, TurneeService service)
            : base(dependencies)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var turnee = _service.GetAll();
            return View(turnee);
        }

        [HttpGet]
        public IActionResult GroupStage(string categorie)
        {
            // Get all matches for the specified category
            var meciuri = _service.GetMeciuriByCategorie(categorie);
            
            // Calculate group standings
            var grupe = _service.CalculateGroupStandings(meciuri.Where(d => d.IdGrupa != Guid.Parse("d9c7cbe8-4c79-48c1-aefc-bf2047789f07") && d.IdGrupa != Guid.Parse("dddb5f22-7ef4-4061-9b16-23994269bbb5")).ToList());

            // Get upcoming matches
            var upcomingMatches = _service.GetUpcomingMatches(categorie);

            ViewBag.Categorie = categorie;
            ViewBag.Grupe = grupe;
            ViewBag.UpcomingMatches = upcomingMatches;

            return View();
        }

        [HttpGet]
        public IActionResult EliminationStage(string categorie)
        {
            // Get all matches for the specified category
            var meciuri = _service.GetMeciuriByCategorie(categorie);

            // Calculate group standings
            var grupe = _service.CalculateGroupStandings(meciuri);

            // Group IDs for different types of matches
            var eliminationGroupId = Guid.Parse("d9c7cbe8-4c79-48c1-aefc-bf2047789f07");
            var classificationGroupId = Guid.Parse("dddb5f22-7ef4-4061-9b16-23994269bbb5");

            // Check if elimination matches already exist for this category
            var existingEliminationMatches = _service.GetMeciuriByCategorie(categorie)
                .Where(m => m.IdGrupa == eliminationGroupId)
                .ToList();

            // Check if classification matches already exist for this category
            var existingClassificationMatches = _service.GetMeciuriByCategorie(categorie)
                .Where(m => m.IdGrupa == classificationGroupId)
                .ToList();

            if ((existingEliminationMatches.Count == 0 || existingClassificationMatches.Count == 0) && grupe != null)
            {
                switch (categorie)
                {
                    case "2010":
                        if (grupe.ContainsKey("Grupa A"))
                        {
                            var grupa2010Standings = grupe["Grupa A"];
                            var topTeams = grupa2010Standings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-01-01 11:10"),
                                    IdEchipaGazda = GetTeamId(topTeams[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeams[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T1"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };
                                _service.CreateMatch(eliminationMatch);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-01-01 10:00"),
                                    IdEchipaGazda = GetTeamId(topTeams[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeams[3].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T1"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };
                                _service.CreateMatch(classificationMatch);
                            }
                        }
                        break;

                    case "2013":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-02-01 13:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsA[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T1A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-02-01 13:50"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T1A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-02-01 14:30"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var classificationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-02-01 15:20"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[3].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[3].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                                _service.CreateMatch(classificationMatch2);
                            }
                        }
                        break;

                    case "2014":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B") && grupe.ContainsKey("Grupa C"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var grupaCStandings = grupe["Grupa C"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(1).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(1).ToList();
                            var topTeamsC = grupaCStandings.OrderByDescending(t => t.Points).Take(1).ToList();
                            var secondPlaces = new List<dynamic>
 {
 grupaAStandings.OrderByDescending(t => t.Points).Skip(1).First(),
 grupaBStandings.OrderByDescending(t => t.Points).Skip(1).First(),
 grupaCStandings.OrderByDescending(t => t.Points).Skip(1).First()
 }.OrderByDescending(t => t.Points).ToList();
                            var thirdPlaces = new List<dynamic>
 {
 grupaAStandings.OrderByDescending(t => t.Points).Skip(2).First(),
 grupaBStandings.OrderByDescending(t => t.Points).Skip(2).First(),
 grupaCStandings.OrderByDescending(t => t.Points).Skip(2).First()
 }.OrderByDescending(t => t.Points).ToList();
                            var fourthPlaces = new List<dynamic>
                            {
                                grupaAStandings.OrderByDescending(t => t.Points).Skip(3).First(),
                                grupaBStandings.OrderByDescending(t => t.Points).Skip(3).First(),
                                grupaCStandings.OrderByDescending(t => t.Points).Skip(3).First()
                            }.OrderByDescending(t => t.Points).ToList();
                            var fifthPlaces = new List<dynamic>
                            {
                                grupaAStandings.OrderByDescending(t => t.Points).Skip(4).First(),
                            }.OrderByDescending(t => t.Points).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 10:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(secondPlaces[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 10:50"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(secondPlaces[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch3 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 11:40"),
                                    IdEchipaGazda = GetTeamId(topTeamsC[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(thirdPlaces[0].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch4 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 12:30"),
                                    IdEchipaGazda = GetTeamId(secondPlaces[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(thirdPlaces[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                                _service.CreateMatch(eliminationMatch3);
                                _service.CreateMatch(eliminationMatch4);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 09:00"),
                                    IdEchipaGazda = GetTeamId(thirdPlaces[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(fifthPlaces[0].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var classificationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 17:00"),
                                    IdEchipaGazda = GetTeamId(fourthPlaces[1].TeamName),
                                    IdEchipaOaspete = GetTeamId(fourthPlaces[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                                _service.CreateMatch(classificationMatch2);
                            }
                        }
                        break;

                    case "2015":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-04-01 13:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-04-01 13:50"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsA[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-04-01 11:20"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var classificationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-04-01 12:10"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[3].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[3].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                                _service.CreateMatch(classificationMatch2);
                            }
                        }
                        break;

                    case "2016":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B") && grupe.ContainsKey("Grupa C") && grupe.ContainsKey("Grupa D"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var grupaCStandings = grupe["Grupa C"];
                            var grupaDStandings = grupe["Grupa D"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsC = grupaCStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsD = grupaDStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 11:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2C"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 11:40"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsA[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2C"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch3 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 12:20"),
                                    IdEchipaGazda = GetTeamId(topTeamsC[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsD[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2C"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch4 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 13:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsD[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsC[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2C"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                                _service.CreateMatch(eliminationMatch3);
                                _service.CreateMatch(eliminationMatch4);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 10:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var classificationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 10:40"),
                                    IdEchipaGazda = GetTeamId(topTeamsC[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsD[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var classificationMatch3 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-05-01 10:20"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[3].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsC[3].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T2C"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                                _service.CreateMatch(classificationMatch2);
                                _service.CreateMatch(classificationMatch3);
                            }
                        }
                        break;

                    case "2017":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-06-01 13:30"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsA[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-06-01 14:10"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-06-01 12:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[2].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3A"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                            }
                        }
                        break;

                    case "2018":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            if (existingEliminationMatches.Count == 0)
                            {
                                var eliminationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 12:40"),
                                    IdEchipaGazda = GetTeamId(topTeamsB[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsA[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                var eliminationMatch2 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 14:10"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[0].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[1].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = eliminationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(eliminationMatch1);
                                _service.CreateMatch(eliminationMatch2);
                            }

                            if (existingClassificationMatches.Count == 0)
                            {
                                var classificationMatch1 = new CreateTurneuModel
                                {
                                    Data = DateTime.Parse("2025-03-01 12:00"),
                                    IdEchipaGazda = GetTeamId(topTeamsA[3].TeamName),
                                    IdEchipaOaspete = GetTeamId(topTeamsB[2].TeamName),
                                    IdStadion = GetStadiumId("Clinceni T3B"),
                                    IdCategorie = GetCategoryId(categorie),
                                    IdGrupa = classificationGroupId,
                                    ScorGazda = 0,
                                    ScorOaspeti = 0
                                };

                                _service.CreateMatch(classificationMatch1);
                            }
                        }
                        break;
                }
            }

            // Get matches from database for display
            var eliminationStageMatches = _service.GetMeciuriByCategorie(categorie)
                .Where(m =>m.IdGrupa == eliminationGroupId)
                .OrderBy(m => m.Data)
                .ToList();

            var classificationStageMatches = _service.GetMeciuriByCategorie(categorie)
                .Where(m =>m.IdGrupa == classificationGroupId)
                .OrderBy(m => m.Data)
                .ToList();

            ViewBag.EliminationMatches = eliminationStageMatches;
            ViewBag.ClassificationMatches = classificationStageMatches;
            ViewBag.Categorie = categorie;

            return View();
        }

        private void SaveMatchToDatabase(dynamic match, string categorie)
        {
            // Get team IDs
            var echipe = _service.GetEchipeForDropdown();
            var echipaGazda = echipe.FirstOrDefault(e => e.Nume == match.EchipaGazdaNume);
            var echipaOaspete = echipe.FirstOrDefault(e => e.Nume == match.EchipaOaspeteNume);

            // Get stadium ID
            var stadioane = _service.GetStadioaneForDropdown();
            var stadion = stadioane.FirstOrDefault(s => s.Nume == match.StadionNume);

            // Get category ID
            var categorii = _service.GetCategoriiForDropdown();
            var categorieId = categorii.FirstOrDefault(c => c.Nume == categorie);

            if (echipaGazda != null && echipaOaspete != null && stadion != null && categorieId != null)
            {
                // Check if match already exists
                var existingMatches = _service.GetMeciuriByCategorie(categorie);
                var matchExists = existingMatches.Any(m => 
                    m.EchipaGazdaNume == match.EchipaGazdaNume && 
                    m.EchipaOaspeteNume == match.EchipaOaspeteNume && 
                    m.Data == match.Data);

                if (!matchExists)
                {
                    var createModel = new CreateTurneuModel
                    {
                        Data = match.Data,
                        IdEchipaGazda = echipaGazda.Id,
                        IdEchipaOaspete = echipaOaspete.Id,
                        IdStadion = stadion.Id,
                        IdCategorie = categorieId.Id,
                        ScorGazda = match.ScorGazda,
                        ScorOaspeti = match.ScorOaspeti
                    };

                    _service.CreateMatch(createModel);
                }
            }
        }

        private Guid GetTeamId(string teamName)
        {
            var team = _service.GetEchipeForDropdown().FirstOrDefault(e => e.Nume == teamName);
            return team?.Id ?? Guid.Empty;
        }

        private Guid GetStadiumId(string stadiumName)
        {
            var stadium = _service.GetStadioaneForDropdown().FirstOrDefault(s => s.Nume == stadiumName);
            return stadium?.Id ?? Guid.Empty;
        }

        private Guid GetCategoryId(string categoryName)
        {
            var category = _service.GetCategoriiForDropdown().FirstOrDefault(c => c.Nume == categoryName);
            return category?.Id ?? Guid.Empty;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Echipe = new SelectList(_service.GetEchipeForDropdown(), "Id", "Nume");
            ViewBag.Stadioane = new SelectList(_service.GetStadioaneForDropdown(), "Id", "Nume");
            ViewBag.Categorii = new SelectList(_service.GetCategoriiForDropdown(), "Id", "Nume");
            ViewBag.Grupe = new SelectList(_service.GetGrupeForDropdown(), "IdGrupa", "Nume");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTurneuModel model)
        {
            if (ModelState.IsValid)
            {
                _service.CreateMatch(model);
                return RedirectToAction(nameof(Create));
            }

            ViewBag.Echipe = new SelectList(_service.GetEchipeForDropdown(), "Id", "Nume");
            ViewBag.Stadioane = new SelectList(_service.GetStadioaneForDropdown(), "Id", "Nume");
            ViewBag.Categorii = new SelectList(_service.GetCategoriiForDropdown(), "Id", "Nume");
            ViewBag.Grupe = new SelectList(_service.GetGrupeForDropdown(), "IdGrupa", "Nume");
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var turneu = _service.GetById(id);
            if (turneu == null)
            {
                return NotFound();
            }

            return View(turneu);
        }

        [HttpPost]
        public IActionResult Edit(TurneuModel model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var turneu = _service.GetById(id);
            if (turneu == null)
            {
                return NotFound();
            }

            return View(turneu);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateScore([FromBody] UpdateScoreModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.UpdateScore(model.MatchId, model.HomeScore, model.AwayScore);

            if (!result)
            {
                return NotFound("Meciul nu a fost găsit.");
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateNextRoundMatch(
            string homeTeam,
            string awayTeam,
            string date,
            string stadium,
            string category,
            Guid groupId)
        {
            try
            {
                // Get team IDs
                var homeTeamId = GetTeamId(homeTeam);
                var awayTeamId = GetTeamId(awayTeam);
                
                // Get stadium ID
                var stadiumId = GetStadiumId(stadium);
                
                // Get category ID
                var categoryId = GetCategoryId(category);

                if (homeTeamId == Guid.Empty || awayTeamId == Guid.Empty || stadiumId == Guid.Empty || categoryId == Guid.Empty)
                {
                    return Json(new { success = false, message = "Invalid team, stadium, or category" });
                }

                // Check if match already exists
                var existingMatches = _service.GetMeciuriByCategorie(category)
                    .Where(m => m.IdGrupa == groupId)
                    .ToList();

                var matchExists = existingMatches.Any(m => 
                    (m.EchipaGazdaNume == homeTeam && m.EchipaOaspeteNume == awayTeam) ||
                    (m.EchipaGazdaNume == awayTeam && m.EchipaOaspeteNume == homeTeam));

                if (matchExists)
                {
                    return Json(new { success = false, message = "Match already exists" });
                }

                // Create the match
                var createModel = new CreateTurneuModel
                {
                    Data = DateTime.Parse(date),
                    IdEchipaGazda = homeTeamId,
                    IdEchipaOaspete = awayTeamId,
                    IdStadion = stadiumId,
                    IdCategorie = categoryId,
                    IdGrupa = groupId,
                    ScorGazda = 0,
                    ScorOaspeti = 0
                };

                var matchId = _service.CreateMatch2(createModel);

                return Json(new { success = true, matchId = matchId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    public class UpdateScoreModel
    {
        public Guid MatchId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
