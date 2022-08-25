using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crafty.Models;

namespace Crafty.Controllers
{
    public class Order_tblController : Controller
    {
        private Crafty_DBEntities1 db = new Crafty_DBEntities1();

        // GET: Order_tbl
        public ActionResult Index()
        {
            var order_tbl = db.Order_tbl.Include(o => o.Cart_tbl).Include(o => o.Product_tbl).Include(o => o.Payment_tbl).Include(o => o.User_tbl);
            return View(order_tbl.ToList());
        }

        // GET: Order_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_tbl order_tbl = db.Order_tbl.Find(id);
            if (order_tbl == null)
            {
                return HttpNotFound();
            }
            return View(order_tbl);
        }

        // GET: Order_tbl/Create
        public ActionResult Create()
        {
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID");
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name");
            ViewBag.Pay_ID = new SelectList(db.Payment_tbl, "Pay_ID", "Pay_Method");
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image");
            return View();
        }

        // POST: Order_tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_ID,Order_Date,Total,Order_Status,U_ID,P_ID,Cart_ID,Pay_ID")] Order_tbl order_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Order_tbl.Add(order_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", order_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", order_tbl.P_ID);
            ViewBag.Pay_ID = new SelectList(db.Payment_tbl, "Pay_ID", "Pay_Method", order_tbl.Pay_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", order_tbl.U_ID);
            return View(order_tbl);
        }

        // GET: Order_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
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
            return View(order_tbl);
        }

        // POST: Order_tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_ID,Order_Date,Total,Order_Status,U_ID,P_ID,Cart_ID,Pay_ID")] Order_tbl order_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", order_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", order_tbl.P_ID);
            ViewBag.Pay_ID = new SelectList(db.Payment_tbl, "Pay_ID", "Pay_Method", order_tbl.Pay_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", order_tbl.U_ID);
            return View(order_tbl);
        }

        // GET: Order_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_tbl order_tbl = db.Order_tbl.Find(id);
            if (order_tbl == null)
            {
                return HttpNotFound();
            }
            return View(order_tbl);
        }

        // POST: Order_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_tbl order_tbl = db.Order_tbl.Find(id);
            db.Order_tbl.Remove(order_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
