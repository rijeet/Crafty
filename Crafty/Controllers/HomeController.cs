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
        public ActionResult DetailsProduct()
        {
            ViewBag.product_id_forDetails = PID;
            var obj = db.Product_tbl.Find(PID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult DetailsProduct([Bind(Include = "U_ID, P_ID,Quantity,Total_Price")] Cart_tbl cart)
        {
            if (ModelState.IsValid)
            {

                db.Cart_tbl.Add(cart);
                db.SaveChanges();
               
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

      

        [HttpGet]
        public ActionResult Catagory()
        {
            var obj = db.Product_tbl.ToList();
            return View(obj);
        }
        public ActionResult CartItem()
        {
           
            return View();

        }


    }
}