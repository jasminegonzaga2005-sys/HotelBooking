namespace customerBookingAPI.RequestModels
{
    public class CreateBookingRequest
    {
        public int CustomerID { get; set; }

        public int RoomID { get; set; }

        public DateOnly CheckIn { get; set; }

        public DateOnly CheckOut { get; set; }

        public int NumberOfGuests { get; set; }
    }
}
