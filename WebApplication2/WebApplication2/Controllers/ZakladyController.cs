using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.EntityManager;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class ZakladyController : Controller
    {
        // GET: Zaklady
        public ActionResult Index()
        {
            BetSetGetView mod = new BetSetGetView();
            using (var db = new ProjektEntities())
            {
                var season = db.SEASONS.OrderByDescending(m => m.Year).Select(r => r.Season_ID).First();

                var driver = from b in db.DRIVERS
                             join p in db.PARTICIPANTS
                             on b.Driver_ID equals p.Driver_ID
                             where p.Season_ID == season
                             select new { b.Driver_Name };

                var x = driver.ToList().Select(c => new SelectListItem
                {
                    Text = c.Driver_Name,
                    Value = c.Driver_Name
                }).ToList();

                ViewBag.KategoriaList = x;

                string login = User.Identity.Name;
                var be = from b in db.BETS
                         join xa in db.RACES
                         on b.Race_ID equals xa.Race_ID
                         where b.User_ID == login && b.ScorePos1 != null
                         select new { xa.Track };

                var xd = be.ToList().Select(c => new SelectListItem
                {
                    Text = c.Track,
                    Value = c.Track
                }).ToList();

                ViewBag.Wyscig1 = xd;


                mod.betRaces = new List<RacesView>();

                var allRace = from r in db.RACES
                              select new { r.Track, r.Date };
                foreach (var item in allRace)
                    mod.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });
            }

            return View(mod);
        }


        public ActionResult dodaj()
        {

            using (var db = new ProjektEntities())
            {
                var season = db.SEASONS.OrderByDescending(m => m.Year).Select(r => r.Season_ID).First();

                var driver = from b in db.DRIVERS
                             join p in db.PARTICIPANTS
                             on b.Driver_ID equals p.Driver_ID
                             where p.Season_ID == season
                             select new { b.Driver_Name };

                var x = driver.ToList().Select(c => new SelectListItem
                {
                    Text = c.Driver_Name,
                    Value = c.Driver_Name
                }).ToList();

                ViewBag.KategoriaList = x;

                string login = User.Identity.Name;
                var be = from b in db.BETS
                         join xa in db.RACES
                         on b.Race_ID equals xa.Race_ID
                         where b.User_ID == login && b.ScorePos1 != null
                         select new { xa.Track };

                var xd = be.ToList().Select(c => new SelectListItem
                {
                    Text = c.Track,
                    Value = c.Track
                }).ToList();

                ViewBag.Wyscig1 = xd;

            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult dodaj(BetSetGetView itm1)
        {
            if (ModelState.IsValid)
            {

                using (var db = new ProjektEntities())
                {
                    var season = db.SEASONS.OrderByDescending(m => m.Year).Select(r => r.Season_ID).First();

                    var driver = from b in db.DRIVERS
                                 join p in db.PARTICIPANTS
                                 on b.Driver_ID equals p.Driver_ID
                                 where p.Season_ID == season
                                 select new { b.Driver_Name };

                    var x = driver.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Driver_Name,
                        Value = c.Driver_Name
                    }).ToList();

                    ViewBag.KategoriaList = x;

                    string login = User.Identity.Name;
                    var be = from b in db.BETS
                             join xa in db.RACES
                             on b.Race_ID equals xa.Race_ID
                             where b.User_ID == login && b.ScorePos1 != null
                             select new { xa.Track };

                    var xd = be.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Track,
                        Value = c.Track
                    }).ToList();

                    int raceID = 2;

                    ViewBag.Wyscig1 = xd;

                    BetManager betManager = new BetManager();
                    if (betManager.IsBetExists(User.Identity.Name, raceID))
                    {
                        ModelState.AddModelError("", "Nie można zrobić dwa razy zakładu na ten sam wyścig.");
                    }
                    else
                    if (itm1.betSetView.Driver_Name1 != itm1.betSetView.Driver_Name2 && itm1.betSetView.Driver_Name2 != itm1.betSetView.Driver_Name3 && itm1.betSetView.Driver_Name1 != itm1.betSetView.Driver_Name3)
                    {

                        betManager.SetBet(User.Identity.Name, raceID, itm1.betSetView.Driver_Name1, itm1.betSetView.Driver_Name2, itm1.betSetView.Driver_Name3, itm1.betSetView.Driver_Time1);
                        ViewBag.Status = "Zakład został dodany.";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Dane się nie zgadzają, proszę wybrać zawodników jeszcze raz.");
                    }

                }
            }
            return View("Index");
        }
        public ActionResult wyswietl()
        {
            if (ModelState.IsValid)
            {
                using (ProjektEntities db = new ProjektEntities())
                {
                    string login = User.Identity.Name;
                    var be = from b in db.BETS
                             join xa in db.RACES
                             on b.Race_ID equals xa.Race_ID
                             where b.User_ID == login && b.ScorePos1 != null
                             select new { xa.Track };

                    var x = be.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Track,
                        Value = c.Track
                    }).ToList();

                    ViewBag.Wyscig1 = x;

                    var season = db.SEASONS.OrderByDescending(m => m.Year).Select(r => r.Season_ID).First();

                    var driver = from b in db.DRIVERS
                                 join p in db.PARTICIPANTS
                                 on b.Driver_ID equals p.Driver_ID
                                 where p.Season_ID == season
                                 select new { b.Driver_Name };

                    var xd = driver.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Driver_Name,
                        Value = c.Driver_Name
                    }).ToList();

                    ViewBag.KategoriaList = xd;

                }

            }
            return View("Index");
        }


        [HttpPost]
        public ActionResult wyswietl(BetSetGetView wybor)
        {
            using (ProjektEntities db = new ProjektEntities())
            {
                string login = User.Identity.Name;
                var be = from b in db.BETS
                         join xa in db.RACES
                         on b.Race_ID equals xa.Race_ID
                         where b.User_ID == login && b.ScorePos1 != null
                         select new { xa.Track };

                var x = be.ToList().Select(c => new SelectListItem
                {
                    Text = c.Track,
                    Value = c.Track
                }).ToList();


                ViewBag.Wyscig1 = x;
                var season = db.SEASONS.OrderByDescending(m => m.Year).Select(r => r.Season_ID).First();

                var driver = from b in db.DRIVERS
                             join p in db.PARTICIPANTS
                             on b.Driver_ID equals p.Driver_ID
                             where p.Season_ID == season
                             select new { b.Driver_Name };

                var xd = driver.ToList().Select(c => new SelectListItem
                {
                    Text = c.Driver_Name,
                    Value = c.Driver_Name
                }).ToList();

                ViewBag.KategoriaList = xd;


                var ras = from rac in db.RACES
                          where rac.Track == wybor.betGetView.selectedTrack
                          from b in db.BETS
                          where (b.User_ID == login) && (b.Race_ID == rac.Race_ID)
                          select new { b.Pos_1, b.Pos_2, b.Pos_3, b.Time_1, b.ScorePos1, b.ScorePos2, b.ScorePos3, b.ScoreTime1, b.ScoreSum };


                foreach (var ds in ras)
                {
                    wybor.betGetView.betPos1 = ds.Pos_1;
                    wybor.betGetView.betPos2 = ds.Pos_2;
                    wybor.betGetView.betPos3 = ds.Pos_3;
                    wybor.betGetView.betTime1 = ds.Time_1;
                    wybor.betGetView.scorePos1 = ds.ScorePos1.Value;
                    wybor.betGetView.scorePos2 = ds.ScorePos2.Value;
                    wybor.betGetView.scorePos3 = ds.ScorePos3.Value;
                    wybor.betGetView.scoreTime1 = ds.ScoreTime1.Value;
                    wybor.betGetView.scoreSum = ds.ScoreSum.Value;
                }

                var races = from rac in db.RACES
                            join dr in db.DRIVERS
                            on rac.Pos_1 equals dr.Driver_ID
                            where rac.Track == wybor.betGetView.selectedTrack
                            select new { dr.Driver_Name };
                foreach (var ds in races)
                {
                    wybor.betGetView.racePos1 = ds.Driver_Name;
                }

                var races1 = from rac in db.RACES
                             where rac.Track == wybor.betGetView.selectedTrack
                             join dr in db.DRIVERS
                            on rac.Pos_2 equals dr.Driver_ID

                             select new { dr.Driver_Name };

                foreach (var ds in races1)
                {
                    wybor.betGetView.racePos2 = ds.Driver_Name;

                }

                var races2 = from rac in db.RACES
                             join dr in db.DRIVERS
                             on rac.Pos_3 equals dr.Driver_ID
                             where rac.Track == wybor.betGetView.selectedTrack
                             select new { dr.Driver_Name };
                foreach (var ds in races2)
                {
                    wybor.betGetView.racePos3 = ds.Driver_Name;
                }

                var races3 = from rac in db.RACES
                             join dr in db.DRIVERS
                             on rac.Time_1 equals dr.Driver_ID
                             where rac.Track == wybor.betGetView.selectedTrack
                             select new { dr.Driver_Name };
                foreach (var ds in races3)
                {
                    wybor.betGetView.raceTime1 = ds.Driver_Name;
                }

            }

            return View(wybor);
        }
    }
}