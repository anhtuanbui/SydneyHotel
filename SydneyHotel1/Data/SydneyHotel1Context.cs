using System.Data.Entity;

namespace SydneyHotel1.Data
{
    public class SydneyHotel1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public SydneyHotel1Context() : base("name=SydneyHotel1Context")
        {
        }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Gender> Genders { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Availability> Availabilities { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Booking> Bookings { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Time> Times { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.EventTime> EventTimes { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.EventType> EventTypes { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.EventRegister> EventRegisters { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.EventRole> EventRoles { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.RoomType> RoomTypes { get; set; }

        public System.Data.Entity.DbSet<SydneyHotel.Models.Documentation> Documentations { get; set; }
    }
}
