using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SydneyHotel1.Controllers
{
    public class EventRoleController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // GET: EventRole
        public ActionResult Index()
        {
            return View(db.EventRoles.ToList());
        }

        // GET: EventRole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRole eventRole = db.EventRoles.Find(id);
            if (eventRole == null)
            {
                return HttpNotFound();
            }
            return View(eventRole);
        }

        // GET: EventRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ObjectName")] EventRole eventRole)
        {
            if (ModelState.IsValid)
            {
                db.EventRoles.Add(eventRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventRole);
        }

        // GET: EventRole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRole eventRole = db.EventRoles.Find(id);
            if (eventRole == null)
            {
                return HttpNotFound();
            }
            return View(eventRole);
        }

        // POST: EventRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ObjectName")] EventRole eventRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventRole);
        }

        // GET: EventRole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRole eventRole = db.EventRoles.Find(id);
            if (eventRole == null)
            {
                return HttpNotFound();
            }
            return View(eventRole);
        }

        // POST: EventRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventRole eventRole = db.EventRoles.Find(id);
            db.EventRoles.Remove(eventRole);
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
