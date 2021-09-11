using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SydneyHotel.Models
{
    public class Time : MyObject
    {
        public int Value { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}