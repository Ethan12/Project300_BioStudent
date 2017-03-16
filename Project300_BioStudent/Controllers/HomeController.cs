using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project300_BioStudent.DAL;
using Project300_BioStudent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project300_BioStudent.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
    }
}