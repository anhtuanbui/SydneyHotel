using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SydneyHotel1.Controllers
{
    public class EventRegisterController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // GET: EventRegister
        public ActionResult Index()
        {
            var eventRegisters = db.EventRegisters.Include(e => e.Account).Include(e => e.Event).Include(e => e.EventRole);
            return View(eventRegisters.ToList());
        }

        // GET: EventRegister/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegister eventRegister = db.EventRegisters.Find(id);
            if (eventRegister == null)
            {
                return HttpNotFound();
            }
            return View(eventRegister);
        }

        // GET: EventRegister/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName");
            ViewBag.EventId = new SelectList(db.Events, "Id", "Description");
            ViewBag.EventRoleId = new SelectList(db.EventRoles, "Id", "ObjectName");
            return View();
        }

        // POST: EventRegister/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventRoleId,EventId,AccountId")] EventRegister eventRegister)
        {
            if (ModelState.IsValid)
            {
                db.EventRegisters.Add(eventRegister);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", eventRegister.AccountId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Description", eventRegister.EventId);
            ViewBag.EventRoleId = new SelectList(db.EventRoles, "Id", "ObjectName", eventRegister.EventRoleId);
            return View(eventRegister);
        }

        // GET: EventRegister/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegister eventRegister = db.EventRegisters.Find(id);
            if (eventRegister == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", eventRegister.AccountId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Description", eventRegister.EventId);
            ViewBag.EventRoleId = new SelectList(db.EventRoles, "Id", "ObjectName", eventRegister.EventRoleId);
            return View(eventRegister);
        }

        // POST: EventRegister/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventRoleId,EventId,AccountId")] EventRegister eventRegister)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventRegister).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", eventRegister.AccountId);
            ViewBag.EventId = new SelectList(db.Events, "Id", "Description", eventRegister.EventId);
            ViewBag.EventRoleId = new SelectList(db.EventRoles, "Id", "ObjectName", eventRegister.EventRoleId);
            return View(eventRegister);
        }

        // GET: EventRegister/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegister eventRegister = db.EventRegisters.Find(id);
            if (eventRegister == null)
            {
                return HttpNotFound();
            }
            return View(eventRegister);
        }

        // POST: EventRegister/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventRegister eventRegister = db.EventRegisters.Find(id);
            db.EventRegisters.Remove(eventRegister);
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
