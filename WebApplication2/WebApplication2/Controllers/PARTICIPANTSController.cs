using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.DB;

namespace WebApplication2.Controllers
{
    public class PARTICIPANTSController : Controller
    {
        private ProjektEntities db = new ProjektEntities();

        // GET: PARTICIPANTS
        public ActionResult Index()
        {
            var pARTICIPANTS = db.PARTICIPANTS.Include(p => p.DRIVERS).Include(p => p.SEASONS).Include(p => p.TEAMS);
            return View(pARTICIPANTS.ToList());
        }

        // GET: PARTICIPANTS/Details/5
        public ActionResult Details(int? id)
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

        // GET: PARTICIPANTS/Create
        public ActionResult Create()
        {
            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name");
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year");
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name");
            return View();
        }

        // POST: PARTICIPANTS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Participants_ID,Season_ID,Driver_ID,Team_ID")] PARTICIPANTS pARTICIPANTS)
        {
            if (ModelState.IsValid)
            {
                db.PARTICIPANTS.Add(pARTICIPANTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", pARTICIPANTS.Driver_ID);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", pARTICIPANTS.Season_ID);
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name", pARTICIPANTS.Team_ID);
            return View(pARTICIPANTS);
        }

        // GET: PARTICIPANTS/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: PARTICIPANTS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Participants_ID,Season_ID,Driver_ID,Team_ID")] PARTICIPANTS pARTICIPANTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTICIPANTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Driver_ID = new SelectList(db.DRIVERS, "Driver_ID", "Driver_Name", pARTICIPANTS.Driver_ID);
            ViewBag.Season_ID = new SelectList(db.SEASONS, "Season_ID", "Year", pARTICIPANTS.Season_ID);
            ViewBag.Team_ID = new SelectList(db.TEAMS, "Team_ID", "Team_Name", pARTICIPANTS.Team_ID);
            return View(pARTICIPANTS);
        }

        // GET: PARTICIPANTS/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: PARTICIPANTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PARTICIPANTS pARTICIPANTS = db.PARTICIPANTS.Find(id);
            db.PARTICIPANTS.Remove(pARTICIPANTS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
