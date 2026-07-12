namespace customerBookingAPI.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }         //pk
        public string RoomTypeName { get; set; }
        public string Description { get; set;}
        public int MaxCapacity {  get; set;}
        public decimal PricePerNight { get; set;}

        

    }
}
