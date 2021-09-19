using SydneyHotel.Models;
using SydneyHotel1.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SydneyHotel1.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        
        private SydneyHotel1Context db = new SydneyHotel1Context();

        [AllowAnonymous]
        public ActionResult List(string text = "")
        {
            var events = db.Events.Include(e => e.EventTime).Include(e => e.EventType).Include(e => e.Room)
                .Where(e => e.ObjectName.Contains(text) || e.Description.Contains(text));
            return View(events);
        }

        // GET: Event/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            if (Session["ID"] != null)
            {

                int accountID = (int)Session["ID"];

                var er = db.EventRegisters.Where(r => r.EventId == (int)id && r.AccountId == accountID).FirstOrDefault();


                if (er != null)
                {
                    ViewBag.EventRegister = er;

                }
            }
            var organizers = db.EventRegisters.Include(r => r.Account).Where(r => r.EventId == (int)id && r.EventRoleId == 2);
            ViewBag.Organisers = organizers;

            var attedees = db.EventRegisters.Include(r => r.Account).Where(r => r.EventId == (int)id && r.EventRoleId == 1);
            ViewBag.Attendees = attedees;

            return View(@event);
        }


        // GET: Event/Create
        public ActionResult Create()
        {
            if ((string)Session["Role"] == "Admin")
            {
                ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Id");

            }
            else if (Session["ID"] != null)
            {
                int accountId = (int)Session["ID"];
                var rooms = db.Bookings.Where(b => b.AccountId == accountId);
                if (rooms.FirstOrDefault() == null)
                {
                    ModelState.AddModelError("", "You need to book a room first");
                }
                ViewBag.RoomId = new SelectList(rooms, "RoomId", "RoomID");


            }
            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView");
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomId,Description,EventTypeId,StartDate,EndDate,EventTimeId,ObjectName")] Event @event)
        {
            if (ModelState.IsValid)
            {
                if (Session["ID"] != null)
                {
                    int accountID = (int)Session["ID"];
                    EventRegister eventRegister = new EventRegister();
                    eventRegister.AccountId = accountID;
                    eventRegister.EventId = @event.Id;
                    eventRegister.EventRoleId = 2;
                    db.EventRegisters.Add(eventRegister);
                }
                db.Events.Add(@event);
                db.SaveChanges();


                return RedirectToAction("List", "Event");
            }

            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView", @event.EventTimeId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description", @event.EventTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "ObjectName", @event.RoomId);

            return View(@event);
        }


        // GET: Event
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.EventTime).Include(e => e.EventType).Include(e => e.Room);
            return View(events.ToList());
        }

        // GET: Event/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView", @event.EventTimeId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description", @event.EventTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "ObjectName", @event.RoomId);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,RoomId,Description,EventTypeId,StartDate,EndDate,EventTimeId,ObjectName")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView", @event.EventTimeId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description", @event.EventTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "ObjectName", @event.RoomId);
            return View(@event);
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
