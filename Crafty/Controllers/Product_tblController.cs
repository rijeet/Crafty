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
    public class Product_tblController : Controller
    {
        private Crafty_DBEntities1 db = new Crafty_DBEntities1();

        // GET: Product_tbl
        public ActionResult Index()
        {
            return View(db.Product_tbl.ToList());
        }

        // GET: Product_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_tbl product_tbl = db.Product_tbl.Find(id);
            if (product_tbl == null)
            {
                return HttpNotFound();
            }
            return View(product_tbl);
        }

        // GET: Product_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "P_ID,Product_Name,Product_Image,Product_Base_Price,Product_Offer_Price,Product_Discount,Category,Features,Product_Descriptation,Product_Stock,Product_Sold")] Product_tbl product_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Product_tbl.Add(product_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_tbl);
        }

        // GET: Product_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_tbl product_tbl = db.Product_tbl.Find(id);
            if (product_tbl == null)
            {
                return HttpNotFound();
            }
            return View(product_tbl);
        }

        // POST: Product_tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_ID,Product_Name,Product_Image,Product_Base_Price,Product_Offer_Price,Product_Discount,Category,Features,Product_Descriptation,Product_Stock,Product_Sold")] Product_tbl product_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_tbl);
        }

        // GET: Product_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_tbl product_tbl = db.Product_tbl.Find(id);
            if (product_tbl == null)
            {
                return HttpNotFound();
            }
            return View(product_tbl);
        }

        // POST: Product_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_tbl product_tbl = db.Product_tbl.Find(id);
            db.Product_tbl.Remove(product_tbl);
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
