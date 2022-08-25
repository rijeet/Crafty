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
    public class Cart_tblController : Controller
    {
        private Crafty_DBEntities1 db = new Crafty_DBEntities1();

        // GET: Cart_tbl
        public ActionResult Index()
        {
            var cart_tbl = db.Cart_tbl.Include(c => c.Product_tbl).Include(c => c.User_tbl);
            return View(cart_tbl.ToList());
        }

        // GET: Cart_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_tbl cart_tbl = db.Cart_tbl.Find(id);
            if (cart_tbl == null)
            {
                return HttpNotFound();
            }
            return View(cart_tbl);
        }

        // GET: Cart_tbl/Create
        public ActionResult Create()
        {
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name");
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image");
            return View();
        }

        // POST: Cart_tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cart_ID,P_ID,Quantity,Total_Price,U_ID")] Cart_tbl cart_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Cart_tbl.Add(cart_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", cart_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", cart_tbl.U_ID);
            return View(cart_tbl);
        }

        // GET: Cart_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_tbl cart_tbl = db.Cart_tbl.Find(id);
            if (cart_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", cart_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", cart_tbl.U_ID);
            return View(cart_tbl);
        }

        // POST: Cart_tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cart_ID,P_ID,Quantity,Total_Price,U_ID")] Cart_tbl cart_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.P_ID = new SelectList(db.Product_tbl, "P_ID", "Product_Name", cart_tbl.P_ID);
            ViewBag.U_ID = new SelectList(db.User_tbl, "U_ID", "Image", cart_tbl.U_ID);
            return View(cart_tbl);
        }

        // GET: Cart_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart_tbl cart_tbl = db.Cart_tbl.Find(id);
            if (cart_tbl == null)
            {
                return HttpNotFound();
            }
            return View(cart_tbl);
        }

        // POST: Cart_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart_tbl cart_tbl = db.Cart_tbl.Find(id);
            db.Cart_tbl.Remove(cart_tbl);
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
