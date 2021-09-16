using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SydneyHotel.Models
{
    public class Event : MyObject
    {
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public string Description { get; set; }

        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start date is required.")]
        [Display(Name = "Start Date")]
        [StringLength(64)]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End date is required.")]
        [StringLength(64)]
        public string EndDate { get; set; }

        public int EventTimeId { get; set; }
        public virtual EventTime EventTime { get; set; }

        public virtual ICollection<EventRegister> EventRegisters { get; set; }

    }

    public class EventType : MyObject
    {
        [Display(Name = "Event Type")]
        public string Description { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }

    public class EventTime
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string EventTimeView { get; set; }

        public int Value { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}