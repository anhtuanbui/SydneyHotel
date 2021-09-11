﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SydneyHotel.Models;
using SydneyHotel1.Data;

namespace SydneyHotel1.Controllers
{
    public class RoomController : Controller
    {
        private SydneyHotel1Context db = new SydneyHotel1Context();

        public void UploadImage(Room room)
        {
            if (room.ImageFile != null)
            {
                // Image File controller 
                // File name string
                string fileName = Path.GetFileNameWithoutExtension(room.ImageFile.FileName);
                string fileExtension = Path.GetExtension(room.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + fileExtension;

                // set file name
                room.Image = fileName;

                // Set file to the path and save file
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                room.ImageFile.SaveAs(fileName);
            }
        }

        // GET: Room
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Availability).Include(r => r.RoomType);
            return View(rooms.ToList());
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
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
            return View(room);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            ViewBag.AvailabilityId = new SelectList(db.Availabilities, "Id", "ObjectName");
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "ObjectName");
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,ImageFile,RoomTypeId,AvailabilityId,Space,Priority,ObjectName")] Room room)
        {
            if (ModelState.IsValid)
            {
                UploadImage(room);

                // Save object to the database
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AvailabilityId = new SelectList(db.Availabilities, "Id", "ObjectName", room.AvailabilityId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "ObjectName", room.RoomTypeId);
            return View(room);
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AvailabilityId = new SelectList(db.Availabilities, "Id", "ObjectName", room.AvailabilityId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "ObjectName", room.RoomTypeId);
            ViewBag.Image = room.Image;
            return View(room);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,ImageFile,RoomTypeId,AvailabilityId,Space,Priority,ObjectName")] Room room)
        {
            if (ModelState.IsValid)
            {
                UploadImage(room);

                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AvailabilityId = new SelectList(db.Availabilities, "Id", "ObjectName", room.AvailabilityId);
            ViewBag.RoomTypeId = new SelectList(db.RoomTypes, "Id", "ObjectName", room.RoomTypeId);
            return View(room);
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
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
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
