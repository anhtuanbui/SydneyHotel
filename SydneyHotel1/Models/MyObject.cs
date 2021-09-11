using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SydneyHotel.Models
{
    public class MyObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string ObjectName { get; set; }
    }
}