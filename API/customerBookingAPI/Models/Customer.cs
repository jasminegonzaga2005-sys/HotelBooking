namespace customerBookingAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email    { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
