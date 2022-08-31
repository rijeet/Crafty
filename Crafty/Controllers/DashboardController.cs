using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Crafty.Models;

namespace Crafty.Controllers
{
    public class DashboardController : Controller
    {
        Crafty_DBEntities1 db = new Crafty_DBEntities1();
        int U_ID= Crafty.Controllers.AuthenticationController.UID;
        // GET: Dashboard
        public ActionResult Index()
        {
            
            try
            {
                if (Session["Role"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
           
            ViewBag.P_ID = new SelectList(db.Order_tbl, "P_ID", "P_ID");
           
              




               
          
                var obj = db.Order_tbl.ToList();
            return View(obj);
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            var obj = db.User_tbl.Find(U_ID);
            return View(obj);
        }
        [HttpPost]
        public new ActionResult Profile(User_tbl user)
        {
           string filename = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string extension = Path.GetExtension(user.ImageFile.FileName);
           filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
           user.Image = "~/img/UserImg/" + filename;
         filename = Path.Combine(Server.MapPath("~/img/UserImg/"), filename);
         user.ImageFile.SaveAs(filename);
            
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
          //  db.User_tbl.Add(user);
           // db.SaveChanges();
            return View();
        }

        public ActionResult Product_List()
        {
            var obj = db.Product_tbl.ToList();
            return View(obj);
        }
        [HttpGet]

        public ActionResult UserDisplay(User_tbl user)
        {
            var obj = db.User_tbl.ToList();
            return View(obj);
        }


        [HttpGet]
        public ActionResult UserDelete(int id)
        {
            var obj = db.User_tbl.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult UserDelete(User_tbl user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            var obj = db.User_tbl.Find(id);
            return View(obj);
        }

        [HttpGet]

        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product_tbl product)
        {
            string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            product.Product_Image = "~/img/ProductImage/" + filename;
            filename = Path.Combine(Server.MapPath("~/img/ProductImage/"), filename);
            product.ImageFile.SaveAs(filename);

            if (ModelState.IsValid)
            {
                db.Product_tbl.Add(product);
                db.SaveChanges();
                return RedirectToAction("Product_List", "Dashboard");
            }
            return View();
        }


       



        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var obj = db.Product_tbl.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult EditProduct(Product_tbl product)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View();
        }



        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            var obj = db.Product_tbl.Find(id);
            db.Product_tbl.Remove(obj);
           db.SaveChanges();
            return View(obj);
        }
        [HttpPost]
        public ActionResult DeleteProduct(Product_tbl product)
        {
           
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DetailsProduct(int id)
        {
            var obj = db.Product_tbl.Find(id);
            return View(obj);
        }

    }
}