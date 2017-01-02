using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;
using WebApplication2.Models.ViewModel;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private ProjektEntities db = new ProjektEntities();

        public ActionResult Index()
        {
            return RedirectToAction("Panel");
        }
        public ViewResult Panel()
        {
            return View();
        }



        /////////////////////////////////////Sekcja dotyczaca wyscigow//////////////////
        public ActionResult Wyscigi()
        {
            var rACES = db.RACES.Include(r => r.SEASONS).Include(r => r.DRIVERS).Include(r => r.DRIVERS2).Include(r => r.DRIVERS3);
            return View(rACES.ToList());
        }

        public ActionResult Szczegoly_wyscigu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACES rACES = db.RACES.Find(id);
            if (rACES == null)
            {
                return HttpNotFound();
            }
            return View(rACES);
        }

        // GET: RACES/Create
        public ActionResult Stworz_wyscig()
        {
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year");
            ViewBag.Pos_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            ViewBag.Pos_2 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            ViewBag.Pos_3 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            ViewBag.Time_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_wyscig([Bind(Include = "Race_ID,Season_ID,Track,Date,Pos_1,Time_1,Pos_2,Pos_3")] RACES rACES)
        {
            if (ModelState.IsValid)
            {
                db.RACES.Add(rACES);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", rACES.Season_ID);
            ViewBag.Pos_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_1);
            ViewBag.Pos_2 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_2);
            ViewBag.Pos_3 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_3);
            ViewBag.Time_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Time_1);
            return View(rACES);
        }

        public ActionResult Edytuj_wyscig(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACES rACES = db.RACES.Find(id);
            if (rACES == null)
            {
                return HttpNotFound();
            }
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", rACES.Season_ID);
            ViewBag.Pos_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_1);
            ViewBag.Pos_2 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_2);
            ViewBag.Pos_3 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_3);
            ViewBag.Time_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Time_1);
            return View(rACES);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_wyscig([Bind(Include = "Race_ID,Season_ID,Track,Date,Pos_1,Time_1,Pos_2,Pos_3")] RACES rACES)
        {
            if (ModelState.IsValid)
            {
                if (rACES.Pos_1 != rACES.Pos_2 && rACES.Pos_1 != rACES.Pos_3 && rACES.Pos_2 != rACES.Pos_3)
                {
                    
                    var bet = from b in db.BETS
                              join r in db.RACES
                              on b.Race_ID equals r.Race_ID
                              where r.Race_ID == rACES.Race_ID
                              select new { b.Pos_1, b.Pos_2, b.Pos_3, b.Bet_ID, b.User_ID };

                    var dri1 = from r in db.RACES
                              from dr in db.DRIVERS
                              where r.Pos_1 == dr.Driver_ID
                              select new { dr.Driver_Name };
                    string pos1 = "";
                    foreach (var a in dri1)
                        pos1 = a.Driver_Name;

                    var dri2 = from r in db.RACES
                               from dr in db.DRIVERS
                               where r.Pos_2 == dr.Driver_ID
                               select new { dr.Driver_Name };
                    string pos2 = "";
                    foreach (var a in dri2)
                        pos2 = a.Driver_Name;


                    var dri3 = from r in db.RACES
                               from dr in db.DRIVERS
                               where r.Pos_3 == dr.Driver_ID
                               select new { dr.Driver_Name };
                    string pos3 = "";
                    foreach (var a in dri3)
                        pos3 = a.Driver_Name;

                    var dri4 = from r in db.RACES
                               from dr in db.DRIVERS
                               where r.Time_1 == dr.Driver_ID
                               select new { dr.Driver_Name };
                    string time1 ="";
                    foreach (var a in dri4)
                        time1 = a.Driver_Name;
                    
                        foreach (var x in bet)
                        {
                            BETS bets = db.BETS.Find(x.Bet_ID);
                        if (bets.Pos_1 == pos1)
                            bets.ScorePos1 = 5;
                        else
                            bets.ScorePos1 = 0;

                        if (bets.Pos_2 == pos2)
                            bets.ScorePos2 = 3;
                        else
                            bets.ScorePos2 = 0;

                        if (bets.Pos_3 == pos3)
                            bets.ScorePos3 = 1;
                        else
                            bets.ScorePos3 = 0;

                        if (bets.Time_1 == time1)
                            bets.ScoreTime1 = 5;
                        else
                            bets.ScoreTime1 = 0;

                        bets.ScoreSum = bets.ScorePos1 + bets.ScorePos2 + bets.ScorePos3 + bets.ScoreTime1;
                            db.Entry(bets).State = EntityState.Modified;
                       
                    }
                    db.Entry(rACES).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Dane się nie zgadzają.");
                }

            }
            ViewBag.Time_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Time_1);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", rACES.Season_ID);
            ViewBag.Pos_1 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_1);
            ViewBag.Pos_2 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_2);
            ViewBag.Pos_3 = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", rACES.Pos_3);
            return View(rACES);
        }

        public ActionResult Usun_wyscig(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RACES rACES = db.RACES.Find(id);
            if (rACES == null)
            {
                return HttpNotFound();
            }
            return View(rACES);
        }

        // POST: RACES/Delete/5
        [HttpPost, ActionName("Usun_wyscig")]
        [ValidateAntiForgeryToken]
        public ActionResult Potwierdzenie_usuniecia_wyscigu(int id)
        {
            RACES rACES = db.RACES.Find(id);
            db.RACES.Remove(rACES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ///////////////////////////////////Sekcja dotyczaca sezonow//////////////////////
        public ActionResult Sezony()
        {
            return View(db.SEASONS.ToList());
        }

        public ActionResult Szczegoly_sezonu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEASONS sEASONS = db.SEASONS.Find(id);
            if (sEASONS == null)
            {
                return HttpNotFound();
            }
            return View(sEASONS);
        }

        public ActionResult Stworz_sezon()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_sezon([Bind(Include = "Season_ID,Year")] SEASONS sEASONS)
        {
            if (ModelState.IsValid)
            {
                db.SEASONS.Add(sEASONS);
                db.SaveChanges();
                return RedirectToAction("Sezony");
            }

            return View(sEASONS);
        }

        public ActionResult Edytuj_sezon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEASONS sEASONS = db.SEASONS.Find(id);
            if (sEASONS == null)
            {
                return HttpNotFound();
            }
            return View(sEASONS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_sezon([Bind(Include = "Season_ID,Year")] SEASONS sEASONS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sEASONS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Sezony");
            }
            return View(sEASONS);
        }

        public ActionResult Usun_sezon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEASONS sEASONS = db.SEASONS.Find(id);
            if (sEASONS == null)
            {
                return HttpNotFound();
            }
            return View(sEASONS);
        }

        [HttpPost, ActionName("Usun_sezon")]
        [ValidateAntiForgeryToken]
        public ActionResult PotwierdzenieUsunieciaSe(int id)
        {
            SEASONS sEASONS = db.SEASONS.Find(id);
            db.SEASONS.Remove(sEASONS);
            db.SaveChanges();
            return RedirectToAction("Sezony");
        }

        ///////////////////////////////////Sekcja dotyczaca zawodnikow//////////////////////
        public ActionResult Zawodnicy()
        {

            return View(db.DRIVERS.ToList());
        }

        public ActionResult Szczegoly_zawodnika(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DRIVERS dRIVERS = db.DRIVERS.Find(id);
            if (dRIVERS == null)
            {
                return HttpNotFound();
            }
            return View(dRIVERS);
        }

        public ActionResult Stworz_zawodnika()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_zawodnika([Bind(Include = "Driver_ID,Driver_Name")] DRIVERS dRIVERS)
        {
            if (ModelState.IsValid)
            {
                db.DRIVERS.Add(dRIVERS);
                db.SaveChanges();
                return RedirectToAction("Zawodnicy");
            }

            return View(dRIVERS);
        }

        public ActionResult Edytuj_zawodnika(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DRIVERS dRIVERS = db.DRIVERS.Find(id);
            if (dRIVERS == null)
            {
                return HttpNotFound();
            }
            return View(dRIVERS);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_zawodnika([Bind(Include = "Driver_ID,Driver_Name")] DRIVERS dRIVERS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dRIVERS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Zawodnicy");
            }
            return View(dRIVERS);
        }

        public ActionResult Usun_zawodnika(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DRIVERS dRIVERS = db.DRIVERS.Find(id);
            if (dRIVERS == null)
            {
                return HttpNotFound();
            }
            return View(dRIVERS);
        }

        [HttpPost, ActionName("Usun_zawodnika")]
        [ValidateAntiForgeryToken]
        public ActionResult PotwierdzenieUsunieciaZa(int id)
        {
            DRIVERS dRIVERS = db.DRIVERS.Find(id);
            db.DRIVERS.Remove(dRIVERS);
            db.SaveChanges();
            return RedirectToAction("Zawodnicy");
        }

        //////////////////////////////////Sekcja dotyczaca firm samochodowych////////////////

        public ActionResult Firmy()
        {
            return View(db.TEAMS.ToList());
        }

        public ActionResult Szczegoly_firmy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEAMS tEAMS = db.TEAMS.Find(id);
            if (tEAMS == null)
            {
                return HttpNotFound();
            }
            return View(tEAMS);
        }

        public ActionResult Stworz_firme()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_firme([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.TEAMS.Add(tEAMS);
                db.SaveChanges();
                return RedirectToAction("Firmy");
            }

            return View(tEAMS);
        }

        public ActionResult Edytuj_firme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEAMS tEAMS = db.TEAMS.Find(id);
            if (tEAMS == null)
            {
                return HttpNotFound();
            }
            return View(tEAMS);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_firme([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEAMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Firmy");
            }
            return View(tEAMS);
        }

        public ActionResult Usun_firme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEAMS tEAMS = db.TEAMS.Find(id);
            if (tEAMS == null)
            {
                return HttpNotFound();
            }
            return View(tEAMS);
        }

        [HttpPost, ActionName("Usun_firme")]
        [ValidateAntiForgeryToken]
        public ActionResult PotwierdzenieUsunieciaFr(int id)
        {
            TEAMS tEAMS = db.TEAMS.Find(id);
            db.TEAMS.Remove(tEAMS);
            db.SaveChanges();
            return RedirectToAction("Firmy");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /////////////////////////////////////////////////////Sekcja dotyczaca uzytkownikow////////////        

        public ActionResult Druzyny()
        {
            var pARTICIPANTS = db.PARTICIPANTS.Include(p => p.DRIVERS).Include(p => p.SEASONS).Include(p => p.TEAMS);
            return View(pARTICIPANTS.ToList());
        }

        public ActionResult Szczegoly_druzyny(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPANTS pARTICIPANTS = db.PARTICIPANTS.Find(id);
            if (pARTICIPANTS == null)
            {
                return HttpNotFound();
            }
            return View(pARTICIPANTS);
        }

        public ActionResult Stworz_druzyne()
        {
            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year");
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_druzyne([Bind(Include = "Participants_ID,Season_ID,Driver_ID,Team_ID")] PARTICIPANTS pARTICIPANTS)
        {
            if (ModelState.IsValid)
            {
                db.PARTICIPANTS.Add(pARTICIPANTS);
                db.SaveChanges();
                return RedirectToAction("Druzyny");
            }

            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", pARTICIPANTS.Driver_ID);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", pARTICIPANTS.Season_ID);
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name", pARTICIPANTS.Team_ID);
            return View(pARTICIPANTS);
        }

        public ActionResult Edytuj_druzyne(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPANTS pARTICIPANTS = db.PARTICIPANTS.Find(id);
            if (pARTICIPANTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", pARTICIPANTS.Driver_ID);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", pARTICIPANTS.Season_ID);
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name", pARTICIPANTS.Team_ID);
            return View(pARTICIPANTS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj_druzne([Bind(Include = "Participants_ID,Season_ID,Driver_ID,Team_ID")] PARTICIPANTS pARTICIPANTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTICIPANTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Druzyny");
            }
            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", pARTICIPANTS.Driver_ID);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", pARTICIPANTS.Season_ID);
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name", pARTICIPANTS.Team_ID);
            return View(pARTICIPANTS);
        }

        public ActionResult Usun_druzyne(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPANTS pARTICIPANTS = db.PARTICIPANTS.Find(id);
            if (pARTICIPANTS == null)
            {
                return HttpNotFound();
            }
            return View(pARTICIPANTS);
        }

        [HttpPost, ActionName("Usun_druzyne")]
        [ValidateAntiForgeryToken]
        public ActionResult Potwierdzenie_usunieciaDr(int id)
        {
            PARTICIPANTS pARTICIPANTS = db.PARTICIPANTS.Find(id);
            db.PARTICIPANTS.Remove(pARTICIPANTS);
            db.SaveChanges();
            return RedirectToAction("Druzyny");
        }

        /////////////////////////////////////////////////////Sekcja dotyczaca uzytkownikow///////////

        public ActionResult Uzytkownicy()
        {
            return View();
        }


        public ActionResult Aktualnosci()
        {
            return View();
        }
    }
}