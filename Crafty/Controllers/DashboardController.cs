using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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

            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Username");






            // var obj = db.Order_tbl.ToList();
            var obj = db.Order_tbl.Include(o => o.Cart_tbl).Include(o => o.Product_tbl).Include(o => o.Payment_tbl).Include(o => o.User_tbl);
            return View(obj);
        }

        [HttpGet]
        public new ActionResult Profile()
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
            
            var obj = db.Product_tbl.ToList();
            return View(obj);
        }
      

       



        [HttpGet]

        public ActionResult AddProduct()
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



        

       

        public ActionResult Order_tbl()
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
            
            var obj = db.Order_tbl.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult EditOrder(int? id)
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
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_tbl order_tbl = db.Order_tbl.Find(id);
            if (order_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", order_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", order_tbl.P_ID);
            ViewBag.Pay_ID = new SelectList(db.Payment_tbl, "Pay_ID", "Pay_Method", order_tbl.Pay_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", order_tbl.U_ID);




            var obj = db.Order_tbl.Find(id);
            return View(obj);
        }
        
        
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult EditOrder([Bind(Include = "Order_ID,Order_Date,Total,Order_Status,U_ID,P_ID,Cart_ID,Pay_ID")] Order_tbl order)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Order_tbl", "Dashboard");
                }
                ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", order.Cart_ID);
                ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", order.P_ID);
                ViewBag.Pay_ID = new SelectList(db.Payment_tbl, "Pay_ID", "Pay_Method", order.Pay_ID);
                ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", order.U_ID);
                return View(order);
            }









        public ActionResult Customer()
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
            var obj = db.User_tbl.Include(o => o.Cart_tbl).Include(o => o.Payment_tbl).Include(o => o.Order_tbl).ToList();
            
            return View(obj);
        }

        public ActionResult CartList()
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
            var obj = db.Cart_tbl.Include(c => c.Product_tbl).Include(c => c.User_tbl).ToList();
            

            return View(obj);
        }

        public ActionResult PaymentList()
        {
            

            var obj = db.Payment_tbl.Include(p => p.Cart_tbl).Include(p => p.Product_tbl).Include(p => p.User_tbl).ToList();
            return View(obj);
        }


        // GET: Payment_tbl/Edit/5
        public ActionResult EditPayment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            if (payment_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", payment_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", payment_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", payment_tbl.U_ID);
            return View(payment_tbl);
        }

        // POST: Payment_tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPayment([Bind(Include = "Pay_ID,Pay_Method,Bkash_Number,Transaction_ID,Pay_Date,Pay_Status,Pay_Amount,Cart_ID,P_ID,U_ID")] Payment_tbl payment_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", payment_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", payment_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", payment_tbl.U_ID);
            return View(payment_tbl);
        }

    }
}