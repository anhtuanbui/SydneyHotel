using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SydneyHotel.Models
{
    public class Account
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(64)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(64)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(128, ErrorMessage = "Your email address should only have maximum 128 characters.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "The password should have minimum length of 6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string DateofBirth { get; set; }


        [StringLength(256, ErrorMessage = "Your address should be up to 256 characters.")]
        public string Address { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<EventRegister> EventRegisters { get; set; }
    }

    public class Role : MyObject
    {
        public virtual ICollection<Account> Accounts { get; set; }
    }

    public class Gender : MyObject
    {
        public virtual ICollection<Account> Account { get; set; }
    }
}