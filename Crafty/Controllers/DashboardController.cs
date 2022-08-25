using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crafty.Models;

namespace Crafty.Controllers
{
    public class DashboardController : Controller
    {
        Crafty_DBEntities1 db = new Crafty_DBEntities1();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Display(User_tbl user)
        {
            var obj = db.User_tbl.ToList();
            return View(obj);
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.User_tbl.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(User_tbl user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = db.User_tbl.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Delete(User_tbl user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult Details(int id)
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
               
            }
            return View();
        }


        [HttpGet]
        public ActionResult ShowProduct()
        {
            var obj = db.Product_tbl.ToList();
            return View(obj);
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