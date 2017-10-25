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
    public class SalaryController : Controller
    {
        private UsersDbEntities db = new UsersDbEntities();

        [Authorize]
        public ActionResult Index()
        {
            var salary_db = db.salary_db.Include(s => s.userinfo_db);
            return View(salary_db.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary_db salary_db = db.salary_db.Find(id);
            if (salary_db == null)
            {
                return HttpNotFound();
            }
            return View(salary_db);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.salary_id = new SelectList(db.userinfo_db, "id", "name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,currency,salary_id,salaryAmount")] salary_db salary_db)
        {
            if (ModelState.IsValid)
            {
                DateTime createdAt = new DateTime();
                createdAt = DateTime.Now;
                salary_db.created_at = createdAt;
                db.salary_db.Add(salary_db);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.salary_id = new SelectList(db.userinfo_db, "id", "name", salary_db.salary_id);
            return View(salary_db);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary_db salary_db = db.salary_db.Find(id);
            if (salary_db == null)
            {
                return HttpNotFound();
            }
            ViewBag.salary_id = new SelectList(db.userinfo_db, "id", "name", salary_db.salary_id);
            return View(salary_db);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,currency,salary_id,salaryAmount")] salary_db salary_db)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary_db).State = EntityState.Modified;
                db.Entry(salary_db).Property(x => x.created_at).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.salary_id = new SelectList(db.userinfo_db, "id", "name", salary_db.salary_id);
            return View(salary_db);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary_db salary_db = db.salary_db.Find(id);
            if (salary_db == null)
            {
                return HttpNotFound();
            }
            return View(salary_db);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            salary_db salary_db = db.salary_db.Find(id);
            db.salary_db.Remove(salary_db);
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

        public ActionResult LogOut()
        {
            ViewBag.LogOut = "";
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}
