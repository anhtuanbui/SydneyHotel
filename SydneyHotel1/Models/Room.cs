using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SydneyHotel.Models
{
    public class Room : MyObject
    {
        public string Image { get; set; }

        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }

        public int AvailabilityId { get; set; }
        public virtual Availability Availability { get; set; }

        // Number of seats in the room
        public int Space { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Event> Events { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class RoomType : MyObject
    {
        public virtual ICollection<Room> Rooms { get; set; }
        public double Price { get; set; }
    }

    public class Availability : MyObject
    {
        public virtual ICollection<Room> Rooms { get; set; }
    }
}