using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crafty.Models;

namespace Crafty.Controllers
{
    public class HomeController : Controller
    {
        public static int PID = 0;
        //
        private int the_id = Crafty.Controllers.AuthenticationController.UID;
        Crafty_DBEntities1 db = new Crafty_DBEntities1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            var obj = db.Product_tbl.ToList();
            return View(obj);
        }
        [HttpGet]
        public ActionResult DetailsProduct(int id)
        {
            var obj = db.Product_tbl.Find(id);
            PID = id;
            ViewBag.Product_id = id;
            return View(obj);




        }
        [HttpPost]
        public ActionResult DetailsProduct([Bind(Include = "U_ID, P_ID, Total_Price, Quantity")] Cart_tbl cart)
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }

            if (ModelState.IsValid)
            {



                db.Cart_tbl.Add(cart);
                db.SaveChanges();
                return RedirectToAction("CartItem", "Home");

            }
            return View();
        }


        //


        [HttpGet]
        public ActionResult Product_tblShow(int id)
        {
            var obj = db.Product_tbl.Find(id);
            PID = id;
            ViewBag.Product_id = id;
            return View(obj);
        }
        [HttpPost]
        public ActionResult Product_tblShow()
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
            if (ModelState.IsValid)
            {


                return RedirectToAction("DetailsProduct", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Catagory()
        {
            var obj = db.Product_tbl.ToList();
            return View(obj);
        }
        [HttpGet]
        public ActionResult CartItem()
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }

            var cartmodel = from c in db.Cart_tbl
                            join p in db.Product_tbl on c.P_ID equals p.P_ID
                            where c.U_ID == Crafty.Controllers.AuthenticationController.UID
                            select new CartProduct { cart = c, product = p };
            return View(cartmodel);

        }

        public ActionResult CartDetailsProduct(int id)
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }

            var obj = db.Cart_tbl.Find(id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult CartDetailsProduct(int id, Cart_tbl cart)
        {


            var obj = db.Cart_tbl.Where(temp => temp.Cart_ID == id).FirstOrDefault();
            db.Cart_tbl.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("CartItem");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
            var cartmodel = from c in db.Cart_tbl
                            join p in db.Product_tbl on c.P_ID equals p.P_ID
                            where c.U_ID == Crafty.Controllers.AuthenticationController.UID
                            select new CartProduct { cart = c, product = p };
            return View(cartmodel);
        }
        [HttpPost]
        public ActionResult Payment([Bind(Include = "U_ID,Transaction_ID,Pay_Method,Pay_Date,Pay_Status")] Payment_tbl payment)
        {

            string Transaction = Request["Transaction_ID"];
            int transaction = Convert.ToInt32(Transaction);

            if (ModelState.IsValid)
            {
                db.Payment_tbl.Add(payment);
                db.SaveChanges();



                var obj = db.Cart_tbl.Where(temp => temp.U_ID == Crafty.Controllers.AuthenticationController.UID).ToList();
                db.Cart_tbl.RemoveRange(obj);
                db.SaveChanges();


                return RedirectToAction("Homepage", "Home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OrderShow()
        {
            try
            {
                if (Session["userid"] == null)
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Authentication");
            }
            var obj = db.Payment_tbl.ToList();
            return View(obj);
        }
    }
}