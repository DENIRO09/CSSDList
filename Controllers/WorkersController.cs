using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSSDROPLIST.Models;

namespace CSSDROPLIST.Controllers
{
    public class WorkersController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Workers
        public ActionResult Index()
        {
            var workers = db.Workers.Include(w => w.Branch).Include(w => w.Province);
            return View(workers.ToList());
        }
        
        // GET: Workers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // GET: Workers/Create
        public ActionResult Create()
        {
           
            ViewBag.pID = new SelectList(db.Provinces, "pID", "pname");
            return View();
        }   

        public ActionResult Branches(int pID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Branch> branchlist = db.Branches.Where(x => x.pID == pID).ToList();
            return Json(branchlist, JsonRequestBehavior.AllowGet);
        }
        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "wID,wName,pID,bID")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Workers.Add(worker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bID = new SelectList(db.Branches, "bID", "bName", worker.bID);
            ViewBag.pID = new SelectList(db.Provinces, "pID", "pname", worker.pID);
            return View(worker);
        }

        // GET: Workers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.bID = new SelectList(db.Branches, "bID", "bName", worker.bID);
            ViewBag.pID = new SelectList(db.Provinces, "pID", "pname", worker.pID);
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "wID,wName,pID,bID")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bID = new SelectList(db.Branches, "bID", "bName", worker.bID);
            ViewBag.pID = new SelectList(db.Provinces, "pID", "pname", worker.pID);
            return View(worker);
        }

        // GET: Workers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Worker worker = db.Workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Worker worker = db.Workers.Find(id);
            db.Workers.Remove(worker);
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
