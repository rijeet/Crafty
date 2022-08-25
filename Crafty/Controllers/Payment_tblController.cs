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
    public class Payment_tblController : Controller
    {
        private Crafty_DBEntities1 db = new Crafty_DBEntities1();

        // GET: Payment_tbl
        public ActionResult Index()
        {
            var payment_tbl = db.Payment_tbl.Include(p => p.Cart_tbl).Include(p => p.Product_tbl).Include(p => p.User_tbl);
            return View(payment_tbl.ToList());
        }

        // GET: Payment_tbl/Details/5
        public ActionResult Details(int? id)
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
            return View(payment_tbl);
        }

        // GET: Payment_tbl/Create
        public ActionResult Create()
        {
            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID");
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name");
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image");
            return View();
        }

        // POST: Payment_tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pay_ID,Pay_Method,Bkash_Number,Transaction_ID,Pay_Date,Pay_Status,Pay_Amount,Cart_ID,P_ID,U_ID")] Payment_tbl payment_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Payment_tbl.Add(payment_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cart_ID = new SelectList(db.Cart_tbl, "Cart_ID", "Cart_ID", payment_tbl.Cart_ID);
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", payment_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", payment_tbl.U_ID);
            return View(payment_tbl);
        }

        // GET: Payment_tbl/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "Pay_ID,Pay_Method,Bkash_Number,Transaction_ID,Pay_Date,Pay_Status,Pay_Amount,Cart_ID,P_ID,U_ID")] Payment_tbl payment_tbl)
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

        // GET: Payment_tbl/Delete/5
        public ActionResult Delete(int? id)
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
            return View(payment_tbl);
        }

        // POST: Payment_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            db.Payment_tbl.Remove(payment_tbl);
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
