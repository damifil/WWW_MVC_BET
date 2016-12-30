using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class ZakladyController : Controller
    {
        // GET: Zaklady
        public ActionResult Index()
        {
            using (var db = new ProjektEntities())
            {
                var driver = from b in db.DRIVERS
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
                         where b.User_ID == login
                         select new { xa.Track };

                var xd = be.ToList().Select(c => new SelectListItem
                {
                    Text = c.Track,
                    Value = c.Track
                }).ToList();


                ViewBag.Wyscig1 = xd;
            }
            return View();
        }

        
        public ActionResult dodaj()
        {

            using (var db = new ProjektEntities())
            {
                var driver = from b in db.DRIVERS
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
                         where b.User_ID == login
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
                    var driver = from b in db.DRIVERS
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
                             where b.User_ID == login
                             select new { xa.Track };

                    var xd = be.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Track,
                        Value = c.Track
                    }).ToList();

                    ViewBag.Wyscig1 = xd;
                    BETS itm = new BETS();

                    itm.User_ID = User.Identity.Name;
                    itm.Date = DateTime.Now.ToString();
                    itm.Race_ID = 2;
                    itm.Pos_1 = itm1.betSetView.Driver_Name1;
                    itm.Pos_2 = itm1.betSetView.Driver_Name2;
                    itm.Pos_3 = itm1.betSetView.Driver_Name3;
                    itm.Time_1 = itm1.betSetView.Driver_Time1;
                    db.BETS.Add(itm);
                    db.SaveChanges();
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
                             where b.User_ID == login
                             select new { xa.Track };

                    var x = be.ToList().Select(c => new SelectListItem
                    {
                        Text = c.Track,
                        Value = c.Track
                    }).ToList();

                    ViewBag.Wyscig1 = x;

                    var driver = from b in db.DRIVERS
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
                         where b.User_ID == login
                         select new { xa.Track };

                var x = be.ToList().Select(c => new SelectListItem
                {
                    Text = c.Track,
                    Value = c.Track
                }).ToList();


                ViewBag.Wyscig1 = x;
                var driver = from b in db.DRIVERS
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
                          select new { b.Pos_1, b.Pos_2, b.Pos_3, b.Time_1 };


                foreach (var ds in ras)
                {
                    wybor.betGetView.betPos1 = ds.Pos_1;
                    wybor.betGetView.betPos2 = ds.Pos_2;
                    wybor.betGetView.betPos3 = ds.Pos_3;
                    wybor.betGetView.betTime1 = ds.Time_1;
                }
                System.Diagnostics.Debug.WriteLine("wyswietlanie: " + wybor.betGetView.selectedTrack);
                var races = from rac in db.RACES
                            join dr in db.DRIVERS
                            on rac.Pos_1 equals dr.Driver_ID
                            where rac.Track == wybor.betGetView.selectedTrack
                            select new { dr.Driver_Name };
                foreach (var ds in races)
                {
                    wybor.betGetView.racePos1 = ds.Driver_Name;
                    System.Diagnostics.Debug.WriteLine("wyswietlanie: " + ds.Driver_Name );
                    System.Diagnostics.Debug.WriteLine("wyswietlanie: " + wybor.betGetView.racePos1);
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