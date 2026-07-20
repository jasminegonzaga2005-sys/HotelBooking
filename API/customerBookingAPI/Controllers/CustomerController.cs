using Azure.Core;
using customerBookingAPI.Models;
using customerBookingAPI.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace customerBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {
        private readonly BookingDbContext _context;
        public CustomerController(BookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomer(string email, string password)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CreateCustomerRequest request)
        {
            if (await _context.Customers.AnyAsync(c => c.Email == request.Email))
            {
                return BadRequest("Email is already registered.");
            }

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                PhoneNum = request.PhoneNum,
                CreatedAt = DateTime.UtcNow,
                Role = request.Role,
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }


    }
}
