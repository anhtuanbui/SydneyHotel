using System.Collections.Generic;

namespace SydneyHotel.Models
{
    public class EventRegister
    {
        public int ID { get; set; }

        public int EventRoleId { get; set; }
        public virtual EventRole EventRole { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }

    public class EventRole : MyObject
    {
        public virtual ICollection<EventRegister> EventRegisters { get; set; }
    }
}