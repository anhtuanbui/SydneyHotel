using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
        public ActionResult Register([Bind(Include = "FirstName,LastName,EmailAddress,Password")] Account account)
        {
            account.RoleId = 1;
            account.GenderId = 3;

            //// Fatal warning
            // Password is not secured. Required hashing or encrypt password when push to use

            var query = db.Accounts.Where(q => q.EmailAddress == account.EmailAddress);

            if (query != null)
            {
                ModelState.AddModelError("", "This email is used");
            }

            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
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

            var query = db.Accounts.Include(r => r.Role).Where(a => a.EmailAddress == account.EmailAddress && a.Password == account.Password);

            var user = query.FirstOrDefault<Account>();

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username and Password");
            }
            else
            {
                if (Session["UserName"] != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Session["ID"] = (int)user.ID;
                Session["UserName"] = user.EmailAddress;
                Session["Role"] = user.Role.ObjectName;


                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {
                Session.Clear();
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
