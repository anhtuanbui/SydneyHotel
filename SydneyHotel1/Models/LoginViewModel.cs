using System.ComponentModel.DataAnnotations;

namespace SydneyHotel1.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType("Password")]
        public string Password { get; set; }
    }
}