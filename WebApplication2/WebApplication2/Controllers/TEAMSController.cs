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
    public class TEAMSController : Controller
    {
        private ProjektEntities db = new ProjektEntities();

        // GET: TEAMS
        public ActionResult Index()
        {
            return View(db.TEAMS.ToList());
        }

        // GET: TEAMS/Details/5
        public ActionResult Details(int? id)
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

        // GET: TEAMS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TEAMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.TEAMS.Add(tEAMS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tEAMS);
        }

        // GET: TEAMS/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TEAMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Team_ID,Team_Name")] TEAMS tEAMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEAMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tEAMS);
        }

        // GET: TEAMS/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TEAMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEAMS tEAMS = db.TEAMS.Find(id);
            db.TEAMS.Remove(tEAMS);
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
