namespace customerBookingAPI.Models
{

    public class Room
    {
        public int RoomID {  get; set; }    //pk
        public string RoomNum { get; set; }
        public int FloorNum { get; set; }
       
        public string  Status { get; set; }

        public int RoomTypeID { get; set; } //fk
        public RoomType RoomType { get; set; }
    }
}
