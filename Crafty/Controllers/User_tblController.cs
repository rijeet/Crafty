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
    public class User_tblController : Controller
    {
        private Crafty_DBEntities1 db = new Crafty_DBEntities1();

        // GET: User_tbl
        public ActionResult Index()
        {
            return View(db.User_tbl.ToList());
        }

        // GET: User_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_tbl user_tbl = db.User_tbl.Find(id);
            if (user_tbl == null)
            {
                return HttpNotFound();
            }
            return View(user_tbl);
        }

        // GET: User_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_tbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "U_ID,Image,Firstname,Lastname,Phone,Mail,Dateofbirth,Address,Gender,Username,Password,CreatedOn,Role")] User_tbl user_tbl)
        {
            if (ModelState.IsValid)
            {
                db.User_tbl.Add(user_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_tbl);
        }

        // GET: User_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_tbl user_tbl = db.User_tbl.Find(id);
            if (user_tbl == null)
            {
                return HttpNotFound();
            }
            return View(user_tbl);
        }

        // POST: User_tbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "U_ID,Image,Firstname,Lastname,Phone,Mail,Dateofbirth,Address,Gender,Username,Password,CreatedOn,Role")] User_tbl user_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_tbl);
        }

        // GET: User_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_tbl user_tbl = db.User_tbl.Find(id);
            if (user_tbl == null)
            {
                return HttpNotFound();
            }
            return View(user_tbl);
        }

        // POST: User_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_tbl user_tbl = db.User_tbl.Find(id);
            db.User_tbl.Remove(user_tbl);
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
