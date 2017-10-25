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

        // GET: Salary
        public ActionResult Index()
        {
            var salary_db = db.salary_db.Include(s => s.userinfo_db);
            return View(salary_db.ToList());
        }

        // GET: Salary/Details/5
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

        // GET: Salary/Create
        public ActionResult Create()
        {
            ViewBag.salary_id = new SelectList(db.userinfo_db, "id", "name");
            return View();
        }

        // POST: Salary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Salary/Edit/5
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

        // POST: Salary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Salary/Delete/5
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

        // POST: Salary/Delete/5
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
    }
}
