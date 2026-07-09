using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomID { get; set; }
        public string RoomType { get; set; }
    }
}
