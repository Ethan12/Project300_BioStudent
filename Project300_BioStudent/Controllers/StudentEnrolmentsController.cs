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
    public class StudentEnrolmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentEnrolments
        public ActionResult Index()
        {
            var enrolment = db.Enrolment.Include(s => s.Module).Include(s => s.Student);
            return View(enrolment.ToList());
        }

        // GET: StudentEnrolments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolment studentEnrolment = db.Enrolment.Find(id);
            if (studentEnrolment == null)
            {
                return HttpNotFound();
            }
            return View(studentEnrolment);
        }

        // GET: StudentEnrolments/Create
        public ActionResult Create()
        {
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "ModuleName");
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName");
            return View();
        }

        // POST: StudentEnrolments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModuleId,StudentId")] StudentEnrolment studentEnrolment)
        {
            if (ModelState.IsValid)
            {
                db.Enrolment.Add(studentEnrolment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "ModuleName", studentEnrolment.ModuleId);
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentEnrolment.StudentId);
            return View(studentEnrolment);
        }

        // GET: StudentEnrolments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolment studentEnrolment = db.Enrolment.Find(id);
            if (studentEnrolment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "ModuleName", studentEnrolment.ModuleId);
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentEnrolment.StudentId);
            return View(studentEnrolment);
        }

        // POST: StudentEnrolments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModuleId,StudentId")] StudentEnrolment studentEnrolment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentEnrolment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleId = new SelectList(db.Modules, "Id", "ModuleName", studentEnrolment.ModuleId);
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentEnrolment.StudentId);
            return View(studentEnrolment);
        }

        // GET: StudentEnrolments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEnrolment studentEnrolment = db.Enrolment.Find(id);
            if (studentEnrolment == null)
            {
                return HttpNotFound();
            }
            return View(studentEnrolment);
        }

        // POST: StudentEnrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentEnrolment studentEnrolment = db.Enrolment.Find(id);
            db.Enrolment.Remove(studentEnrolment);
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
