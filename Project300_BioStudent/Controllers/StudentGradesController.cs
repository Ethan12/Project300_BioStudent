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
    public class StudentGradesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentGrades
        public ActionResult Index()
        {
            var studentGrades = db.StudentGrades.Include(s => s.Student);
            return View(studentGrades.ToList());
        }

        // GET: StudentGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            return View(studentGrades);
        }

        // GET: StudentGrades/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName");
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,Grade")] StudentGrades studentGrades)
        {
            if (ModelState.IsValid)
            {
                db.StudentGrades.Add(studentGrades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentGrades.StudentId);
            return View(studentGrades);
        }

        // GET: StudentGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentGrades.StudentId);
            return View(studentGrades);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,Grade")] StudentGrades studentGrades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentGrades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.StudentUserAccounts, "Id", "FullName", studentGrades.StudentId);
            return View(studentGrades);
        }

        // GET: StudentGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            return View(studentGrades);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGrades studentGrades = db.StudentGrades.Find(id);
            db.StudentGrades.Remove(studentGrades);
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
