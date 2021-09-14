using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security;

namespace SydneyHotel1.Controllers
{
    public class AccountController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // Register
        public ActionResult Register()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,FirstName,LastName,EmailAddress,Password,GenderId,DateofBirth,Role")] Account account)
        {
            account.RoleId = 1;
            account.GenderId = 3;

            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            return View(account);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "EmailAddress,Password")] Account account)
        {

            //var accounts = db.Accounts.Include(a => a.Gender).Include(a => a.Role);

            var isvalidUser = account.EmailAddress == "Admin@gmail.com" && account.Password == "123456";

            if (!isvalidUser)
            {
                ModelState.AddModelError("", "Invalid Username and Password");
            }
            else
            {
                if (Request.Cookies["UserName"] != null)
                {
                    return RedirectToAction("Index");
                }

                HttpCookie httpCookie = new HttpCookie("UserName", account.EmailAddress.ToString());

                httpCookie.Expires.AddDays(3);

                HttpContext.Response.SetCookie(httpCookie);

                HttpCookie newCookie = Request.Cookies["UserName"];

                ViewBag.UserName = newCookie;

                return RedirectToAction("Index", "Home");
            }

            
            return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["UserName"].Value != null)
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-5);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Gender).Include(a => a.Role);
            return View(accounts.ToList());
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "ObjectName");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,EmailAddress,Password,GenderId,PhoneNumber,DateofBirth,Address,RoleId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "ObjectName", account.RoleId);
            return View(account);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "ObjectName", account.RoleId);
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,EmailAddress,Password,GenderId,PhoneNumber,DateofBirth,Address,RoleId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "ObjectName", account.RoleId);
            return View(account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
