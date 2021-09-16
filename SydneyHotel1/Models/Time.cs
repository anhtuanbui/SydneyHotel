using System.Collections.Generic;

namespace SydneyHotel.Models
{
    public class Time : MyObject
    {
        public int Value { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}