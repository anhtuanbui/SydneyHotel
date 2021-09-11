using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SydneyHotel.Models;
using SydneyHotel1.Data;

namespace SydneyHotel1.Controllers
{
    public class EventController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // GET: Event
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.EventTime).Include(e => e.EventType).Include(e => e.Room);
            return View(events.ToList());
        }

        // GET: Event/Details/5
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
            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView");
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description");
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "ObjectName");
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
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTimeId = new SelectList(db.EventTimes, "Id", "EventTimeView", @event.EventTimeId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Description", @event.EventTypeId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "ObjectName", @event.RoomId);
            return View(@event);
        }

        // GET: Event/Edit/5
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
