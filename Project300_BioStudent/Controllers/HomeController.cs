using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using System.Data.Entity;
using Project300_BioStudent.Models;

namespace Project300_BioStudent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
             public FileContentResult ProfilePhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                if (id == null)
                {
                    string filename = HttpContext.Server.MapPath(@"~/Images/Blank.png");

                    byte[] imagedata = null;
                    FileInfo fileinfo = new FileInfo(filename);
                    long imageFileLength = fileinfo.Length;
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imagedata = br.ReadBytes((int)imageFileLength);

                    return File(imagedata, "image/png");
                }
                var bdusers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdusers.Users.Where(x => x.Id == id).FirstOrDefault();

                return new FileContentResult(userImage.ProfilePhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }

        }
   }
}