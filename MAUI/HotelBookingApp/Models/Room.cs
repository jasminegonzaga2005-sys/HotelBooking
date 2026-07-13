using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNum { get; set; }
        public int FloorNum { get; set; }
        public string Status { get; set; }
        public int RoomTypeID { get; set; }

        public RoomType RoomType { get; set; }
    }
}
