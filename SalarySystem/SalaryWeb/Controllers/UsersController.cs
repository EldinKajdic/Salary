using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalaryWeb.Models;

namespace SalaryWeb.Controllers
{
    public class UsersController : Controller
    {
        private UsersDbEntities db = new UsersDbEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.userinfo_db.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userinfo_db userinfo_db = db.userinfo_db.Find(id);
            if (userinfo_db == null)
            {
                return HttpNotFound();
            }
            return View(userinfo_db);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password,C_union,current_job")] userinfo_db userinfo_db)
        {
            if (ModelState.IsValid)
            {
                db.userinfo_db.Add(userinfo_db);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userinfo_db);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userinfo_db userinfo_db = db.userinfo_db.Find(id);
            if (userinfo_db == null)
            {
                return HttpNotFound();
            }
            return View(userinfo_db);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,password,C_union,current_job")] userinfo_db userinfo_db)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userinfo_db).State = EntityState.Modified;
                db.Entry(userinfo_db).Property(x => x.created_at).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userinfo_db);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userinfo_db userinfo_db = db.userinfo_db.Find(id);
            if (userinfo_db == null)
            {
                return HttpNotFound();
            }
            return View(userinfo_db);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userinfo_db userinfo_db = db.userinfo_db.Find(id);
            db.userinfo_db.Remove(userinfo_db);
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
