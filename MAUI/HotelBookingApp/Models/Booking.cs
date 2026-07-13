using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Nights { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalCost { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDate { get; set; }



    }
}
