using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project300_BioStudent.Models;

namespace Project300_BioStudent.Controllers
{
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public ActionResult Index()
        {
            var modules = db.Modules.Include(m => m.Lecturers);
            return View(modules.ToList());
        }

        // GET: Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = db.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            ViewBag.LecturerId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModuleName,LecturerId")] Modules modules)
        {
            if (ModelState.IsValid)
            {
                db.Modules.Add(modules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LecturerId = new SelectList(db.Users, "Id", "FullName", modules.LecturerId);
            return View(modules);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = db.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            ViewBag.LecturerId = new SelectList(db.Users, "Id", "FullName", modules.LecturerId);
            return View(modules);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModuleName,LecturerId")] Modules modules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LecturerId = new SelectList(db.Users, "Id", "FullName", modules.LecturerId);
            return View(modules);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modules modules = db.Modules.Find(id);
            if (modules == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modules modules = db.Modules.Find(id);
            db.Modules.Remove(modules);
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
