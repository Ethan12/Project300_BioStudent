using Project300_BioStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project300_BioStudent.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentRegister(StudentUserAccount acc)
        {
            if (ModelState.IsValid)
            {
                using (StudentDbContext db = new StudentDbContext())
                {
                    db.StudentUserAccounts.Add(acc);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = acc.FullName + " " + " Successfully Registered";
            }
            return View();
        }

        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(StudentUserAccount studentUser)
        {
            using (StudentDbContext db = new StudentDbContext())
            {
                try
                {
                    var studentusr = db.StudentUserAccounts.Single(u => u.StudentNum == studentUser.StudentNum && u.Password == studentUser.Password);
                    if (studentusr != null)
                    {
                        Session["Id"] = studentusr.Id.ToString();
                        Session["FullName"] = studentusr.FullName.ToString();
                        Session["FingerprintID"] = studentusr.FingerprintID.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Student Number or Password is incorrect!");
                    }
                }
                catch(InvalidOperationException ie)
                {
                    ModelState.AddModelError("", "Student Number or Password is incorrect!");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("StudentLogin");
            }
        }
    }
}