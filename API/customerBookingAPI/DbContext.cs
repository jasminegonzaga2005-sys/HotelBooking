using Microsoft.EntityFrameworkCore;
using customerBookingAPI.Models;

namespace customerBookingAPI
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext>options)
            : base(options) 
        { 
        }
      
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Booking> Bookings  {get; set; }
    }
}
