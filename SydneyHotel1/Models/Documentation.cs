using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SydneyHotel.Models
{
    public class Documentation
    {
        public int ID { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(256)]
        public string ImgURL { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}