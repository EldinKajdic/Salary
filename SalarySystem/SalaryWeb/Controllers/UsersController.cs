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

        [Authorize]
        public ActionResult Index()
        {
            return View(db.userinfo_db.ToList());
        }

        [Authorize]
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userinfo_db User)
        {

            if (User.email == null || User.password == null)
            {
                ModelState.AddModelError("LoginError", "Enter information in all fields.");
            }
            else
            {
                if (User.email.Contains("@"))
                {


                    if (ModelState.IsValid)
                    {
                        var user = (from users in db.userinfo_db
                                    where users.email == User.email.ToUpper()
                                    && users.password == User.password
                                    select new
                                    {
                                        users.id,
                                        users.email
                                    }).ToList();

                        if (user.FirstOrDefault() != null)
                        {
                            bool trueUser = false;
                            trueUser = validateLogin(User.email, User.password);

                            if (trueUser == true)
                            {

                                Session["id"] = user.FirstOrDefault().id;
                                var sessionID = (int)Session["id"];
                                Session["email"] = user.FirstOrDefault().email;
                                ViewBag.SessionEmail = Session["email"];
                                ViewBag.LogOut = "Sign out";

                                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(User.email, false);

                            }

                            return RedirectToAction("Index", "Users");
                        }
                        else
                        {
                            ModelState.AddModelError("LoginError", "The email or password you entered was not correct.");
                            ViewBag.SessionEmail = "";
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("LoginError", "This is not a valid email.");
                    ViewBag.SessionEmail = "";
                }

            }


            return View(User);
        }

        public ActionResult LogOut()
        {
            ViewBag.LogOut = "";
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private bool validateLogin(string username, string password)
        {
            var user = from users in db.userinfo_db
                       where users.email.ToUpper() == username.ToUpper()
                       && users.password == password
                       select users;

            if (user.Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,password,C_union,current_job")] userinfo_db userinfo_db)
        {
            if (userinfo_db.name == null || userinfo_db.email == null || userinfo_db.password == null)
            {
                ModelState.AddModelError("RegisterError", "Enter information in all fields.");
            }

            else
            {
                if (userinfo_db.email.Contains("@"))
                {
                    if (userinfo_db.password.Length >= 6)
                    {
                        if (ModelState.IsValid)
                        {
                            DateTime createdAt = new DateTime();
                            createdAt = DateTime.Now;
                            userinfo_db.created_at = createdAt;
                            userinfo_db.current_job = "No job presented";
                            userinfo_db.C_union = "No union presented";

                            db.userinfo_db.Add(userinfo_db);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RegisterError", "The password must have atleast 6 characters.");
                    }

                }
                else
                {
                    ModelState.AddModelError("RegisterError", "This is not a valid email.");
                }

            }

            return View(userinfo_db);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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
