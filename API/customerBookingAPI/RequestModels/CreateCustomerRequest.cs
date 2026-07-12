using System.ComponentModel.DataAnnotations;

namespace customerBookingAPI.RequestModels
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
    }
}
