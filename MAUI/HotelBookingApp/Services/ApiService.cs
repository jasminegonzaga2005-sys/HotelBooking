using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using HotelBookingApp.Models;


namespace HotelBookingApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //customer endpoints
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer");
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Customer", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        //rooms endpoints
        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Room>>("api/Rooms");
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Room>($"api/Rooms/{id}");
        }

        public async Task UpdateRoomAsync(int id, Room updatedRoom)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/Rooms/{id}", updatedRoom);
            response.EnsureSuccessStatusCode();
        }

        //booking endpoints
        public async Task<List<Booking>> GetBookingsAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<Booking>>($"api/Booking/customer/{id}");
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Booking", booking);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Booking>();
        }
    }
}
