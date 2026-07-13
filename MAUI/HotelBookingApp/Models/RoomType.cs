using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
