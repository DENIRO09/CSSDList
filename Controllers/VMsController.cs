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
    public class VMsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: VMs
        public ActionResult Index()
        {
            return View(db.VMs.ToList());
        }

        // GET: VMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VM vM = db.VMs.Find(id);
            if (vM == null)
            {
                return HttpNotFound();
            }
            return View(vM);
        }

        // GET: VMs/Create
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

        // POST: VMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "W1,pID,bID")] VM vM)
        {
            if (ModelState.IsValid)
            {
                db.VMs.Add(vM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vM);
        }

        // GET: VMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VM vM = db.VMs.Find(id);
            if (vM == null)
            {
                return HttpNotFound();
            }
            return View(vM);
        }

        // POST: VMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "W1,pID,bID")] VM vM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vM);
        }

        // GET: VMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VM vM = db.VMs.Find(id);
            if (vM == null)
            {
                return HttpNotFound();
            }
            return View(vM);
        }

        // POST: VMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VM vM = db.VMs.Find(id);
            db.VMs.Remove(vM);
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
