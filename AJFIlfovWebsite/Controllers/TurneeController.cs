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
            var grupe = _service.CalculateGroupStandings(meciuri);

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

            List<dynamic> eliminationMatches = new List<dynamic>();
            List<dynamic> classificationMatches = new List<dynamic>();

            if (grupe != null)
            {
                switch (categorie)
                {
                    case "2010":
                        if (grupe.ContainsKey("Grupa A"))
                        {
                            var grupa2010Standings = grupe["Grupa A"];
                            var topTeams = grupa2010Standings.OrderByDescending(t => t.Points).Take(4).ToList();

                            eliminationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeams[0].TeamName, EchipaOaspeteNume = topTeams[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-01-01 10:00"),
                                    StadionNume = "Clinceni"
                                },
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeams[2].TeamName, EchipaOaspeteNume = topTeams[3].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-01-01 11:00"),
                                    StadionNume = "Clinceni"
                                }
                            };
                        }
                        break;

                    case "2013":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            eliminationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = topTeamsB[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-02-01 13:00"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = topTeamsA[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-02-01 13:50"),
                                    StadionNume = "Clinceni"
                                }
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[2].TeamName, EchipaOaspeteNume = topTeamsB[2].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-02-01 14:40"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[3].TeamName, EchipaOaspeteNume = topTeamsB[3].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-02-01 14:40"),
                                    StadionNume = "Clinceni"
                                }
                            };
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

                            eliminationMatches = new List<dynamic>
                            {
                                new { EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = secondPlaces[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 10:00"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = secondPlaces[2].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 10:50"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsC[0].TeamName, EchipaOaspeteNume = thirdPlaces[0].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 11:40"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = secondPlaces[0].TeamName, EchipaOaspeteNume = thirdPlaces[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 12:30"), StadionNume = "Clinceni" }
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new { EchipaGazdaNume = thirdPlaces[2].TeamName, EchipaOaspeteNume = fourthPlaces[0].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 13:20"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = fourthPlaces[1].TeamName, EchipaOaspeteNume = fourthPlaces[2].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 13:20"), StadionNume = "Clinceni" }
                            };
                        }
                        break;

                    case "2015":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            eliminationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = topTeamsB[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-04-01 13:00"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = topTeamsA[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-04-01 13:50"),
                                    StadionNume = "Clinceni"
                                }
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[2].TeamName, EchipaOaspeteNume = topTeamsB[2].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-04-01 14:40"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[3].TeamName, EchipaOaspeteNume = topTeamsB[3].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-04-01 14:40"),
                                    StadionNume = "Clinceni"
                                }
                            };
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

                            eliminationMatches = new List<dynamic>
                            {
                                new { EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = topTeamsB[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 11:00"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = topTeamsA[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 11:40"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsC[0].TeamName, EchipaOaspeteNume = topTeamsD[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 12:20"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsD[0].TeamName, EchipaOaspeteNume = topTeamsC[1].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 13:00"), StadionNume = "Clinceni" },
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new { EchipaGazdaNume = topTeamsA[2].TeamName, EchipaOaspeteNume = topTeamsB[2].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 13:40"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsC[2].TeamName, EchipaOaspeteNume = topTeamsD[2].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 14:20"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsA[3].TeamName, EchipaOaspeteNume = topTeamsB[3].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 13:40"), StadionNume = "Clinceni" },
                                new { EchipaGazdaNume = topTeamsC[3].TeamName, EchipaOaspeteNume = topTeamsD[3].TeamName, ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-05-01 14:20"), StadionNume = "Clinceni" },
                            };
                        }
                        break;

                    case "2017":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            eliminationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = topTeamsB[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-06-01 13:30"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = topTeamsA[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-06-01 14:10"),
                                    StadionNume = "Clinceni"
                                }
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[2].TeamName, EchipaOaspeteNume = topTeamsB[2].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-06-01 14:50"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[3].TeamName, EchipaOaspeteNume = topTeamsB[3].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-06-01 14:50"),
                                    StadionNume = "Clinceni"
                                }
                            };
                        }
                        break;

                    case "2018":
                        if (grupe.ContainsKey("Grupa A") && grupe.ContainsKey("Grupa B"))
                        {
                            var grupaAStandings = grupe["Grupa A"];
                            var grupaBStandings = grupe["Grupa B"];
                            var topTeamsA = grupaAStandings.OrderByDescending(t => t.Points).Take(4).ToList();
                            var topTeamsB = grupaBStandings.OrderByDescending(t => t.Points).Take(4).ToList();

                            eliminationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[0].TeamName, EchipaOaspeteNume = topTeamsB[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 13:30"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsB[0].TeamName, EchipaOaspeteNume = topTeamsA[1].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 14:10"),
                                    StadionNume = "Clinceni"
                                }
                            };

                            classificationMatches = new List<dynamic>
                            {
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[2].TeamName, EchipaOaspeteNume = topTeamsB[2].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 14:50"),
                                    StadionNume = "Clinceni"
                                },
                                new
                                {
                                    EchipaGazdaNume = topTeamsA[3].TeamName, EchipaOaspeteNume = topTeamsB[3].TeamName,
                                    ScorGazda = 0, ScorOaspeti = 0, Data = DateTime.Parse("2025-03-01 14:50"),
                                    StadionNume = "Clinceni"
                                }
                            };
                        }
                        break;
                }
            }

            ViewBag.EliminationMatches = eliminationMatches;
            ViewBag.ClassificationMatches = classificationMatches;
            ViewBag.Categorie = categorie;

            return View();
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
    }

    public class UpdateScoreModel
    {
        public Guid MatchId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
