using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Crafty.Models;


namespace Crafty.Controllers
{
    public class AuthenticationController : Controller
    {
        public static int UID = 0;
        
        Crafty_DBEntities1 db = new Crafty_DBEntities1();
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Authentication Auth)
        {
            if (ModelState.IsValid == true)
            {


                var credential = db.User_tbl.Where(model => model.Username == Auth.UserName && model.Password == Auth.Password).FirstOrDefault();


                //select Role from User_tbl where Username='Rijeet'
                
                var credential1 = db.User_tbl.Where(u => u.Username == Auth.UserName).FirstOrDefault();
              
         UID = credential1.U_ID;
        Console.WriteLine(credential);
                if (credential == null)
                {
                    TempData["msg"] = "Incorrect Password / Username";
                    return View();
                }

                else if (credential1.Role == "Admin")
                {
                    Session["userPic"] = credential1.Image;
                    Session["userid"] = Auth.UserName;
                    Session["Role"] = credential1.Role;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {

                    Session["userPic"] = credential1.Image;
                    Session["userid"] = Auth.UserName;
                    Session["Role"] = credential1.Role;
                    return RedirectToAction("HomePage", "Home");
                }


            }

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration( User_tbl user)
        {
            string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string extension = Path.GetExtension(user.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            user.Image = "~/img/UserImg/" + filename;
            filename = Path.Combine(Server.MapPath("~/img/UserImg/"), filename);
            user.ImageFile.SaveAs(filename);

            if (ModelState.IsValid)
            {




                var credential1 = (from c in db.User_tbl where c.Username.Equals(user.Username) select c).SingleOrDefault();
                var credential2 = (from c in db.User_tbl where c.Mail.Equals(user.Mail) select c).SingleOrDefault();
                var credential3 = (from c in db.User_tbl where c.Phone.Equals(user.Address) select c).SingleOrDefault();

                if (credential1 != null)
                {
                    ViewBag.ErrorMessage = "This username is already registered";
                    return View();
                }
                else if (credential2 != null)
                {
                    ViewBag.ErrorMessage = "This Mail is already registered";
                    return View();
                }
                else if (credential3 != null)
                {
                    ViewBag.ErrorMessage = "This Phone number is already registered";
                    return View();
                }
                else
                {
                    db.User_tbl.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Authentication");

                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }
    }
}