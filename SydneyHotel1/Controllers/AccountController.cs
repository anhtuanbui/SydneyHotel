using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SydneyHotel1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();


        // change password function
        // GET
        public ActionResult ChangePassword()
        {
            int accountID = (int)Session["ID"];
            Account account = db.Accounts.Find(accountID);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            return View(account);
        }


        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "ID,FirstName,LastName,EmailAddress,Password,GenderId,PhoneNumber,DateofBirth,Address,RoleId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }
            return View(account);
        }

        // Edit user profile
        // GET
        public ActionResult EditProfile()
        {
            int accountID = (int)Session["ID"];
            Account account = db.Accounts.Find(accountID);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            return View(account);
        }


        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ID,FirstName,LastName,EmailAddress,Password,GenderId,PhoneNumber,DateofBirth,Address,RoleId")] Account account)
        {

            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName", account.GenderId);
            return View(account);
        }


        // Manage pages for controlling booking and event
        public ActionResult Manage()
        {
            int accountID = (int)Session["ID"];
            var user = db.Accounts.Where(a => a.ID == accountID).FirstOrDefault();

            ViewBag.Bookings = db.Bookings.Include(b => b.Room).Where(b => b.AccountId == accountID);

            ViewBag.EventRegisters = db.EventRegisters.Include(e => e.Event).Where(e => e.AccountId == accountID);

            return View(user);
        }



        // Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "ObjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "FirstName,LastName,EmailAddress,Password")] Account account)
        {
            account.RoleId = 1;
            account.GenderId = 3;

            //// Fatal warning
            // Password is not secured. Required hashing or encrypt password when push to use

            var query = db.Accounts.Where(q => q.EmailAddress == account.EmailAddress).FirstOrDefault();

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

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["ID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "EmailAddress,Password")] Account account)
        {
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
                Session["RoleID"] = user.RoleId;

                FormsAuthentication.SetAuthCookie(user.EmailAddress, false);

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Account
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Gender).Include(a => a.Role);
            return View(accounts.ToList());
        }

        // GET: Account/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
