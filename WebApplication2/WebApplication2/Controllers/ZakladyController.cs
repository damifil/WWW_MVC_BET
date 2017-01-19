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


                var selectYear = from b in db.BETS
                         join xa in db.RACES
                         on b.Race_ID equals xa.Race_ID
                         join da in db.SEASONS
                         on xa.Season_ID equals da.Season_ID
                         where b.User_ID == login && b.ScorePos1 != null
                         group da by da.Year into avc
                         select avc.FirstOrDefault();

                mod.MenuLevel1 = selectYear.ToList().Select(m => new SelectListItem
                {
                    Value = m.Season_ID.ToString(),
                    Text = m.Year
                }).ToList();


                mod.MenuLevel1.Insert(0, new SelectListItem
                {
                    Value = "-1",
                    Text = "Rok"
                });

                mod.MenuLevel2 = new List<SelectListItem>();


                mod.betRaces = new List<RacesView>();

                var allRace = from r in db.RACES
                              select new { r.Track, r.Date };
                foreach (var item in allRace)
                    mod.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });
            }

            return View(mod);
        }

        [HttpGet]
        public ActionResult filterCatLevel2(int id)
        {
            var db = new ProjektEntities();
            var temp = from r in db.RACES
                     where r.Season_ID == id
                     join b in db.BETS
                     on r.Race_ID equals b.Race_ID
                     where b.User_ID == User.Identity.Name && b.ScorePos1 != null
                     select r;

            var selectTrack = temp.ToList().Select(c => new SelectListItem
            {
                Value = c.Track,
                Text = c.Track
            }).ToList();

            selectTrack.Insert(0, new SelectListItem
            {
                Value = "-1",
                Text = "Wyścig"
            });

            return Json(selectTrack, JsonRequestBehavior.AllowGet);
        }

        public ActionResult dodaj()
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

                var selectYear = from b in db.BETS
                                 join xa in db.RACES
                                 on b.Race_ID equals xa.Race_ID
                                 join da in db.SEASONS
                                 on xa.Season_ID equals da.Season_ID
                                 where b.User_ID == User.Identity.Name && b.ScorePos1 != null
                                 group da by da.Year into avc
                                 select avc.FirstOrDefault();



                mod.MenuLevel1 = selectYear.ToList().Select(m => new SelectListItem
                {
                    Value = m.Season_ID.ToString(),
                    Text = m.Year
                }).ToList();


                mod.MenuLevel1.Insert(0, new SelectListItem
                {
                    Value = "-1",
                    Text = "Rok"
                });

                mod.MenuLevel2 = new List<SelectListItem>();

                string login = User.Identity.Name;
               
                mod.betRaces = new List<RacesView>();

                var allRace = from r in db.RACES
                              select new { r.Track, r.Date };
                foreach (var item in allRace)
                    mod.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult dodaj(BetSetGetView itm1)
        {
            BetSetGetView mod = new BetSetGetView();
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

                    var selectYear = from b in db.BETS
                                     join xa in db.RACES
                                     on b.Race_ID equals xa.Race_ID
                                     join da in db.SEASONS
                                     on xa.Season_ID equals da.Season_ID
                                     where b.User_ID == User.Identity.Name && b.ScorePos1 != null
                                     group da by da.Year into avc
                                     select avc.FirstOrDefault();



                    mod.MenuLevel1 = selectYear.ToList().Select(m => new SelectListItem
                    {
                        Value = m.Season_ID.ToString(),
                        Text = m.Year
                    }).ToList();


                    mod.MenuLevel1.Insert(0, new SelectListItem
                    {
                        Value = "-1",
                        Text = "Rok"
                    });

                    mod.MenuLevel2 = new List<SelectListItem>();
                 
                    string login = User.Identity.Name;

                    mod.betRaces = new List<RacesView>();

                    var allRace = from r in db.RACES
                                  select new { r.Track, r.Date };
                    foreach (var item in allRace)
                        mod.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });

                    int raceID = 1;
                    string date = Request.Form["date_picker"];
                    date = date.Replace("/", "-");
                    var searchRaceID = from r in db.RACES
                                       where r.Date.Contains(date)
                                       select new { r.Race_ID, };
                    foreach (var item in searchRaceID)
                        raceID = item.Race_ID;

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
            return View(mod);
        }
        public ActionResult wyswietl()
        {
            if (ModelState.IsValid)
            {
                using (ProjektEntities db = new ProjektEntities())
                {
                    BetSetGetView mod = new BetSetGetView();
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

                    var selectYear = from b in db.BETS
                                     join xa in db.RACES
                                     on b.Race_ID equals xa.Race_ID
                                     join da in db.SEASONS
                                     on xa.Season_ID equals da.Season_ID
                                     where b.User_ID == login && b.ScorePos1 != null
                                     group da by da.Year into avc
                                     select avc.FirstOrDefault();

                    mod.MenuLevel1 = selectYear.ToList().Select(m => new SelectListItem
                    {
                        Value = m.Season_ID.ToString(),
                        Text = m.Year
                    }).ToList();


                    mod.MenuLevel1.Insert(0, new SelectListItem
                    {
                        Value = "-1",
                        Text = "Rok"
                    });

                    mod.MenuLevel2 = new List<SelectListItem>();

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
                   
                    mod.betRaces = new List<RacesView>();

                    mod.betRaces = new List<RacesView>();
                    var allRace = from r in db.RACES
                                  select new { r.Track, r.Date };
                    foreach (var item in allRace)
                        mod.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });
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
               
                var selectYear = from b in db.BETS
                                 join xa in db.RACES
                                 on b.Race_ID equals xa.Race_ID
                                 join da in db.SEASONS
                                 on xa.Season_ID equals da.Season_ID
                                 where b.User_ID == login && b.ScorePos1 != null
                                 group da by da.Year into avc
                                 select avc.FirstOrDefault();



                wybor.MenuLevel1 = selectYear.ToList().Select(m => new SelectListItem
                {
                    Value = m.Season_ID.ToString(),
                    Text = m.Year
                }).ToList();


                wybor.MenuLevel1.Insert(0, new SelectListItem
                {
                    Value = "-1",
                    Text = "Rok"
                });

                wybor.MenuLevel2 = new List<SelectListItem>();

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
                          where rac.Track == wybor.CategoryLevel2
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
                            where rac.Track == wybor.CategoryLevel2
                            select new { dr.Driver_Name };
                foreach (var ds in races)
                {
                    wybor.betGetView.racePos1 = ds.Driver_Name;
                }

                var races1 = from rac in db.RACES
                             where rac.Track == wybor.CategoryLevel2
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
                             where rac.Track == wybor.CategoryLevel2
                             select new { dr.Driver_Name };
                foreach (var ds in races2)
                {
                    wybor.betGetView.racePos3 = ds.Driver_Name;
                }

                var races3 = from rac in db.RACES
                             join dr in db.DRIVERS
                             on rac.Time_1 equals dr.Driver_ID
                             where rac.Track == wybor.CategoryLevel2
                             select new { dr.Driver_Name };
                foreach (var ds in races3)
                {
                    wybor.betGetView.raceTime1 = ds.Driver_Name;
                }
                wybor.betRaces = new List<RacesView>();
                var allRace = from r in db.RACES
                              select new { r.Track, r.Date };
                foreach (var item in allRace)
                    wybor.betRaces.Add(new RacesView { raceTrack = item.Track, raceDate = item.Date });
            }

            return View(wybor);
        }
    }
}