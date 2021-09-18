using SydneyHotel.Models;
using SydneyHotel1.Data;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SydneyHotel1.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        // Cancel booking for user
        
        public ActionResult Cancel(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Manage", "Account");
        }
        // Allow user to use this page
        // GET
        [AllowAnonymous]
        public ActionResult Room(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            ViewBag.Room = room;

            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Room([Bind(Include = "ID,NumberOfPeople,StartDate,EndDate,TimeId")] Booking booking, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            ViewBag.Room = room;
            booking.RoomID = (int)id;
            booking.AccountId = (int)Session["ID"];

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Manage", "Account");
            }

            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName", booking.TimeId);
            return View(booking);
        }


        // GET: Booking
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Account).Include(b => b.Room).Include(b => b.Time);
            return View(bookings.ToList());
        }

        // GET: Booking/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName");
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Image");
            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,RoomID,AccountId,NumberOfPeople,StartDate,EndDate,TimeId,Message")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", booking.AccountId);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Image", booking.RoomID);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName", booking.TimeId);
            return View(booking);
        }

        // GET: Booking/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", booking.AccountId);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Image", booking.RoomID);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName", booking.TimeId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,RoomID,AccountId,NumberOfPeople,StartDate,EndDate,TimeId,Message")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "ID", "FirstName", booking.AccountId);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Image", booking.RoomID);
            ViewBag.TimeId = new SelectList(db.Times, "Id", "ObjectName", booking.TimeId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
