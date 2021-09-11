using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SydneyHotel.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int RoomID { get; set; }
        public virtual Room Room { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        [Required(ErrorMessage = "Number of people is required.")]
        [Display(Name = "Number of People")]
        public int NumberOfPeople { get; set; }

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

        public int TimeId { get; set; }
        public virtual Time Time { get; set; }

        public string Message { get; set; }
    }
}