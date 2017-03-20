using Project300_BioStudent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Project300_BioStudent.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET: Enroll
        public ActionResult Enroll()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    return View(db.StudentUserAccounts.ToList());
                }
            }catch(ModelValidationException mve)
            {
                return View();
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Enroll(StudentUserAccount ac)
        {
            string busyUrl = "https://api.particle.io/v1/devices/1c002b000d47343432313031/isbusy/?access_token=f3665e22952ac82b1e7e9b1d5929b25f66915673";
            string json = "";
            using (var client = new WebClient())
            {
                json = client.DownloadString(busyUrl);
            }

            dynamic jsonDecoded = JObject.Parse(json);

            if (jsonDecoded.result != 1)
            {
                string triggerUrl = "https://maker.ifttt.com/trigger/enroll_jeef/with/key/o-arvDPNPh5XbneGdmQLWn17n80o919r3WjxjTGWV2-";
                string userUrl = "https://api.particle.io/v1/devices/1c002b000d47343432313031/userid/?access_token=f3665e22952ac82b1e7e9b1d5929b25f66915673";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"identity", "1"}
                    };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(triggerUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                }

                System.Threading.Thread.Sleep(8000);

                var userId = -1;

                string jsonUser = "";
                using (var client = new WebClient())
                {
                    jsonUser = client.DownloadString(userUrl);
                }

                dynamic jsonDecodedUser = JObject.Parse(jsonUser);

                userId = jsonDecodedUser.result;

                if (userId != -1)
                {

                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        try
                        {
                            var studentUser = db.StudentUserAccounts.Single(u => u.StudentNum == ac.StudentNum);
                            if(studentUser != null)
                            {
                                studentUser.FingerprintID = userId;
                                db.SaveChanges();
                                return RedirectToAction("EnrollSuccess");
                            }
                        }catch(InvalidOperationException ie)
                        {
          
                        }
                    }
                }else
                {
                    return RedirectToAction("EnrollFail");
                }

            }else
            {
                //PhotonBusy
                ModelState.AddModelError("", "The device is busy, please try again later.");
            }

            return View();
        }

        public ActionResult StudentRegister()
        {
            return View();
        }

        public ActionResult EnrollSuccess()
        {
            return View();
        }

        public ActionResult EnrollFail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentRegister(StudentUserAccount acc)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
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
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var studentusr = db.StudentUserAccounts.Single(u => u.StudentNum == studentUser.StudentNum && u.Password == studentUser.Password);
                    if (studentusr != null)
                    {
                        Session["Id"] = studentusr.Id.ToString();
                        Session["FullName"] = studentusr.FullName.ToString();
                        Session["StudentNum"] = studentusr.StudentNum.ToString();
                        Session["FingerprintID"] = studentusr.FingerprintID.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Student Number or Password is incorrect!");
                    }
                }
                catch (InvalidOperationException ie)
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