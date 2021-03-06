using System.ComponentModel.DataAnnotations;

namespace SydneyHotel.Models
{
    public class MyObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Name")]
        public string ObjectName { get; set; }
    }
}