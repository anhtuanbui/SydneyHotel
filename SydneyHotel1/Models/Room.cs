using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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

        public string Values()
        {
            List<string> values = new List<string>();
            values.Add(this.Id.ToString());
            values.Add(this.ObjectName);
            values.Add(this.Image);
            values.Add(this.RoomTypeId.ToString());
            values.Add(this.AvailabilityId.ToString());
            values.Add(this.Space.ToString());
            values.Add(this.Priority.ToString());
            return string.Join(", ", values.ToArray());
        }
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