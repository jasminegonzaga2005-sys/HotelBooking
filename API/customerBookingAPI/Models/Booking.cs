namespace customerBookingAPI.Models
{
    public class Booking
    {
        public int BookingID { get; set; } //pk
        public int CustomerID { get; set; }//fk
        public Customer Customer { get; set;}
        public int RoomID { get; set; }//fk
        public Room Room { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly Checkout { get; set; }
        public int Nights { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalCost { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDate { get; set; }


    }
}
