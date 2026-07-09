using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.Models;
namespace HotelBookingApp.Services
{
    public class BookingService
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5226")
        };

        public async Task CreateBookingAsync(Booking booking)
        {
            await _httpClient.PostAsJsonAsync("/api/bookings", booking);
        }

        //public async Task<List<Booking>> GetBookingsAsync()
        //{
        //    return new List<Booking>();
        //}

        //public async Task CancelBookingAsync(Booking booking)
        //{

        //}
    }
}
