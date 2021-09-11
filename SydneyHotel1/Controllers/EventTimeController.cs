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
    public class EventTimeController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // GET: EventTime
        public ActionResult Index()
        {
            return View(db.EventTimes.ToList());
        }

        // GET: EventTime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTime eventTime = db.EventTimes.Find(id);
            if (eventTime == null)
            {
                return HttpNotFound();
            }
            return View(eventTime);
        }

        // GET: EventTime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventTime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventTimeView,Value")] EventTime eventTime)
        {
            if (ModelState.IsValid)
            {
                db.EventTimes.Add(eventTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventTime);
        }

        // GET: EventTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTime eventTime = db.EventTimes.Find(id);
            if (eventTime == null)
            {
                return HttpNotFound();
            }
            return View(eventTime);
        }

        // POST: EventTime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventTimeView,Value")] EventTime eventTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventTime);
        }

        // GET: EventTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTime eventTime = db.EventTimes.Find(id);
            if (eventTime == null)
            {
                return HttpNotFound();
            }
            return View(eventTime);
        }

        // POST: EventTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventTime eventTime = db.EventTimes.Find(id);
            db.EventTimes.Remove(eventTime);
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
