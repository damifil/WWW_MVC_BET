using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private ProjektEntities db = new ProjektEntities();
        // GET: Admin
        public ViewResult Panel()
        {
            return View();
        }
        
        
        public ActionResult Wyscigi()
        {
            return View();
        }        
        public ActionResult Sezony()
        {
            return View();
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

        //////////////////////////////////Sekcja dotyczaca tylko druzyn////////////////

        public ActionResult Druzyny()
        {
            return View(db.TEAMS.ToList());
        }

        public ActionResult Szczegoly_druzyny(int? id)
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

        public ActionResult Stworz_druzyne()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stworz_druzyne([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.TEAMS.Add(tEAMS);
                db.SaveChanges();
                return RedirectToAction("Druzyny");
            }

            return View(tEAMS);
        }

        public ActionResult Edytuj_druzyne(int? id)
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
        public ActionResult Edytuj_druzyne([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEAMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Druzyny");
            }
            return View(tEAMS);
        }

        public ActionResult Usun_druzyne(int? id)
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

        [HttpPost, ActionName("Usun_druzyne")]
        [ValidateAntiForgeryToken]
        public ActionResult PotwierdzenieUsunieciaDr(int id)
        {
            TEAMS tEAMS = db.TEAMS.Find(id);
            db.TEAMS.Remove(tEAMS);
            db.SaveChanges();
            return RedirectToAction("Druzyny");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
                
        /////////////////////////////////////////////////////Sekcja dotyczaca uzytkownikow
        
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